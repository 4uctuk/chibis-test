using System.Threading.Tasks;

namespace ChibisTest.Features.Cart
{
    public interface ICartExpiredService
    {
        /// <summary>
        /// Remove expired carts (running by timed hosted service)
        /// </summary>
        /// <returns></returns>
        Task RemoveExpiredCarts();
    }
}
