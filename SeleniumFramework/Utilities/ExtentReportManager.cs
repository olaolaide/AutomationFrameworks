using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SeleniumFramework.Utilities;

public static class ExtentReportManager
{
    private static ExtentReports _extent;
    private static ExtentSparkReporter _sparkReporter;
    public static ExtentTest Feature;
    public static ExtentTest Scenario;

    public static void InitReport()
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            $"TestReports/ExtentReport_{timestamp}.html");

        Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);

        _sparkReporter = new ExtentSparkReporter(reportPath);
        _extent = new ExtentReports();
        _extent.AttachReporter(_sparkReporter);
    }

    public static ExtentReports GetReporter() =>
        _extent ?? throw new InvalidOperationException("ExtentReports is not initialized.");

    public static void FlushReport() => _extent.Flush();
}