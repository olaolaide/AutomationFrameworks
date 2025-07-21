using OpenQA.Selenium;

namespace SeleniumFramework.Utilities;

public static class ScreenshotHelper
{
    public static string TakeScreenshot(IWebDriver driver, string scenarioTitle)
    {
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        var fileName = $"{scenarioTitle}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var screenshotFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net8.0", ""),
            "ScreenshotFolder");

        Directory.CreateDirectory(screenshotFolder);

        var fullPath = Path.Combine(screenshotFolder, fileName);

        screenshot.SaveAsFile(fullPath);

        return fullPath;
    }
}