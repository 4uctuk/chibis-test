using System;
using System.Linq;
using System.Threading.Tasks;
using ChibisTest.Features.DataAccess;
using ChibisTest.Features.WebHooks;
using Microsoft.Extensions.Logging;

namespace ChibisTest.Features.Cart
{
    public class CartExpiredService : ICartExpiredService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHooksService _hooksService;
        private readonly ILogger<CartExpiredService> _logger;

        public CartExpiredService(ApplicationDbContext context, IHooksService hooksService, ILogger<CartExpiredService> logger)
        {
            _context = context;
            _hooksService = hooksService;
            _logger = logger;
        }

        public async Task RemoveExpiredCarts()
        {
            var expiredCarts = _context.Carts.Where(c => c.CreatedDateTime.AddDays(30) < DateTime.UtcNow).ToList();

            foreach (var expiredCart in expiredCarts)
            {
                try
                {
                    _context.Carts.Remove(expiredCart);
                    _context.SaveChanges();
                    await _hooksService.SendDeleteMessage(expiredCart);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}
