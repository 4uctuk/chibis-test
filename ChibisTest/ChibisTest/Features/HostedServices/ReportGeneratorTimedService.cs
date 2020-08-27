using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ChibisTest.Features.Cart;
using ChibisTest.Features.Report;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChibisTest.Features.HostedServices
{
    public class ReportGeneratorTimedService : IHostedService, IDisposable
    {
        private readonly ILogger<ReportGeneratorTimedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public ReportGeneratorTimedService(ILogger<ReportGeneratorTimedService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ReportGeneratorTimedService running.");
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(20),
                TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetService<IReportExporter>();
                service.ExportAndSaveReport();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ReportGeneratorTimedService is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
