using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ChibisTest.Features.Common.Exceptions;
using ChibisTest.Features.DataAccess;
using ChibisTest.Features.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChibisTest.Features.WebHooks
{
    public class HooksService : IHooksService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _clientFactory;

        public HooksService(ApplicationDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public async Task<List<Subscriber>> GetAllSubscribers()
        {
            var result = await _context.Subscribers.ToListAsync();
            return result;
        }

        public async Task AddSubscriber(string url)
        {
            var subscriber = await GetSubscriberByUrl(url);

            if (subscriber != null)
            {
                throw new DuplicatedEntityException();
            }

            await _context.Subscribers.AddAsync(new Subscriber()
            {
                SubscribeUrl = url
            });
            await _context.SaveChangesAsync();
        }

        private async Task<Subscriber> GetSubscriberByUrl(string url)
        {
            var subscriber = await _context.Subscribers.FirstOrDefaultAsync(c =>
                c.SubscribeUrl.ToLower() == url.ToLower());
            return subscriber;
        }

        public async Task RemoveSubscriber(string url)
        {
            var subscriber = await GetSubscriberByUrl(url);

            if (subscriber == null)
            {
                throw new EntityNotFoundException();
            }

            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();
        }

        public async Task SendDeleteMessage(DataAccess.Entities.Cart cart)
        {
            var subscribers = _context.Subscribers.ToList();
            var serializedData = JsonSerializer.Serialize(cart);

            using (var client = _clientFactory.CreateClient())
            {
                foreach (var subscriber in subscribers)
                {
                    var message = new HttpRequestMessage(HttpMethod.Post, subscriber.SubscribeUrl)
                    {
                        Content = new StringContent(serializedData)
                    };
                    await client.SendAsync(message);
                }
              
            }
        }
    }
}
