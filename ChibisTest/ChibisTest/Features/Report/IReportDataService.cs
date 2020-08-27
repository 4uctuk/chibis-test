using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibisTest.Features.Report
{
    public interface IReportDataService
    {
        Task<Report> GetReportData();
    }
}
