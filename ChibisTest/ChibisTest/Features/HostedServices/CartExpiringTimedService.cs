using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChibisTest.Features.Cart;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChibisTest.Features.HostedServices
{
    public class CartExpiringTimedService : IHostedService, IDisposable
    {
        private readonly ILogger<CartExpiringTimedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public CartExpiringTimedService(ILogger<CartExpiringTimedService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CartExpiringTimedService running.");
            _timer = new Timer(DoWork, null, TimeSpan.FromMinutes(1),
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetService<ICartExpiredService>();
                service.RemoveExpiredCarts();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CartExpiringTimedService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
