using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using SeleniumFramework.Utilities;

namespace SeleniumFramework.Hooks;

[Binding]
public class Hook(ScenarioContext scenarioContext)
{
    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var screenshotFolder =
            AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net8.0", "") + @"\ScreenshotFolder";
        if (!Directory.Exists(screenshotFolder))
            Directory.CreateDirectory(screenshotFolder);

        ExtentReportManager.InitReport();
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        ExtentReportManager.Feature = ExtentReportManager
            .GetReporter()
            .CreateTest<Feature>(featureContext.FeatureInfo.Title);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Navigate().GoToUrl("https://www.google.com/");
        
        scenarioContext["driver"] = driver;

        ExtentReportManager.Scenario = ExtentReportManager
            .Feature
            .CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
    }
    
    [AfterStep]
    public void AfterStep()
    {
        var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
        var stepText = scenarioContext.StepContext.StepInfo.Text;

        var keyword = new GherkinKeyword(stepType);

        if (scenarioContext.TestError == null)
        {
            ExtentReportManager.Scenario.CreateNode(keyword, stepText);
        }
        else
        {
            var driver = (IWebDriver)scenarioContext["driver"];
            var screenshotPath = ScreenshotHelper.TakeScreenshot(driver, scenarioContext.ScenarioInfo.Title);

            ExtentReportManager.Scenario
                .CreateNode(keyword, stepText)
                .Fail(scenarioContext.TestError.Message)
                .AddScreenCaptureFromPath(screenshotPath);
        }
    }



    [AfterScenario]
    public void AfterScenario()
    {
        var driver = (IWebDriver)scenarioContext["driver"];
        driver.Quit();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        ExtentReportManager.FlushReport();
    }
}
