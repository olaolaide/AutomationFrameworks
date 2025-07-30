using System.Globalization;
using OpenQA.Selenium;

namespace SeleniumFramework.Utilities;

public class CalendarPicker(IWebDriver driver)
{
    IJavaScriptExecutor _js = (IJavaScriptExecutor)driver;

    public void PickCalendar(string date) // e.g. "07 August 2025"
    {
        var dates = date.Split(" ");
        var day = int.Parse(dates[0]);
        var month = DateTime.ParseExact(dates[1], "MMMM", CultureInfo.InvariantCulture).Month;
        var year = int.Parse(dates[2]);

        var js = (IJavaScriptExecutor)driver;

        // Open the calendar using JS
        var calendarButton = driver.FindElement(By.XPath("//span[contains(@class, 'k-i-calendar')]"));
        js.ExecuteScript("arguments[0].click();", calendarButton);

        // Navigate to the correct year/month
        while (true)
        {
            var calendarHeader = driver.FindElement(By.XPath("//div[contains(@class,'k-calendar-header')]//button")).Text;
            var currentDate = DateTime.ParseExact(calendarHeader, "MMMM yyyy", CultureInfo.InvariantCulture);

            if (currentDate.Year == year && currentDate.Month == month)
            {
                break;
            }
            else if (currentDate.Year < year || (currentDate.Year == year && currentDate.Month < month))
            {
                var next = driver.FindElement(By.XPath("//a[@data-action='next']"));
                js.ExecuteScript("arguments[0].click();", next);
            }
            else
            {
                var prev = driver.FindElement(By.XPath("//a[@data-action='prev']"));
                js.ExecuteScript("arguments[0].click();", prev);
            }

            Thread.Sleep(300); // Slight wait for UI update
        }

        // Click the desired day using JS
        var dayXPath = $"//td[not(contains(@class, 'k-other-month'))]//a[text()='{day}']";
        var dayElement = driver.FindElement(By.XPath(dayXPath));
        js.ExecuteScript("arguments[0].click();", dayElement);
    }


    public void TypeCalendar(string date)
    {
        var selector = driver.FindElement(By.XPath("//input[contains(@id, 'Required')]"));
        var js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].value = '';", selector);

        js.ExecuteScript($"arguments[0].value = '{date}';", selector);
    }
}