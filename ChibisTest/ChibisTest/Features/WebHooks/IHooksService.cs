using System.Collections.Generic;
using System.Threading.Tasks;
using ChibisTest.Features.DataAccess.Entities;

namespace ChibisTest.Features.WebHooks
{
    public interface IHooksService
    {
        /// <summary>
        /// Return all subsribers
        /// </summary>
        /// <returns></returns>
        Task<List<Subscriber>> GetAllSubscribers();

        /// <summary>
        /// Add subscriber
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task AddSubscriber(string url);

        /// <summary>
        /// Remove subscriber
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task RemoveSubscriber(string url);

        /// <summary>
        /// Send cart delete message to all subscribers
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task SendDeleteMessage(DataAccess.Entities.Cart cart);
    }
}
