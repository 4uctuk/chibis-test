using System;
using System.Linq;
using System.Threading.Tasks;
using ChibisTest.Features.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChibisTest.Features.Report
{
    public class ReportDataService : IReportDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReportDataService> _logger;

        public ReportDataService(ApplicationDbContext context, ILogger<ReportDataService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Report> GetReportData()
        {
            try
            {
                var report = new Report();

                var carts = _context.Carts.Include("CartItems.Product").ToList();

                if (carts.Count > 0)
                {
                    report.TotalCount = carts.Count();

                    report.CartsWithBonuses = carts.Count(c => c.CartItems.Any(ci => ci.ForBonusPoints));

                    report.AverageCost = carts.Average(c => c.CartItems.Sum(i => i.Quantity * i.Product.Cost));
                    
                    var dateNow = DateTime.UtcNow;

                    report.ExpiresIn10Days = carts.Count(c => (c.CreatedDateTime.AddDays(30) - dateNow).Days <= 10);
                    report.ExpiresIn20Days = carts.Count(c => (c.CreatedDateTime.AddDays(30) - dateNow).Days <= 20);
                    report.ExpiresIn30Days = carts.Count(c => (c.CreatedDateTime.AddDays(30) - dateNow).Days <= 30);
                }

                return report;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
         
        }
    }
}
