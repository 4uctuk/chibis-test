using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChibisTest.Features.Report
{
    public interface IReportExporter
    {
        /// <summary>
        /// Export report
        /// </summary>
        /// <returns></returns>
        Task ExportReport();
    }
}
