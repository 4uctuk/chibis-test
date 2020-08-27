using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChibisTest.Features.Report
{
    public class TextFileReportExporter : IReportExporter
    {
        private readonly IReportDataService _reportDataService;

        public TextFileReportExporter(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }

        public async Task ExportReport()
        {
            var reportData = _reportDataService.GetReportData();
            var serializedData = JsonSerializer.Serialize(reportData);
            var date = DateTime.UtcNow;
            var fileName = $"Report_{date.Year}_{date.Month}_{date.Day}_{date.Hour}.json";
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.GetEnvironmentVariable("ReportsFolder"));
            var path = Path.Combine(folder, fileName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            if (File.Exists(path))
                File.Delete(path);

            await using var sw = File.CreateText(path);
            await sw.WriteAsync(serializedData);
        }
    }
}
