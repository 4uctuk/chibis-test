namespace ChibisTest.Features.Report
{
    public interface IReportDataService
    {
        /// <summary>
        /// Return report data from database
        /// </summary>
        /// <returns></returns>
        Report GetReportData();
    }
}
