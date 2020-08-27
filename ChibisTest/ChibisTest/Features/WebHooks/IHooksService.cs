using System.Collections.Generic;
using System.Threading.Tasks;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.WebHooks
{
    public interface IHooksService
    {
        Task<List<Subscriber>> GetAllSubscribers();

        Task AddSubscriber(string url);

        Task RemoveSubscriber(string url);

        Task SendDeleteMessage(DataAccess.Entities.Cart cart);
    }
}
