using OpenQA.Selenium;
using System.Collections.Generic;

namespace SeleniumFramework.Pages;

public class GoogleHomePage(IWebDriver driver)
{
    private IWebElement SearchBox => driver.FindElement(By.Name("q"));

    public void EnterSearchText(string text)
    {
        SearchBox.Clear();
        SearchBox.SendKeys(text);
    }

    public IList<IWebElement> GetSearchSuggestions()
    {
        return driver.FindElements(By.CssSelector("ul[role='listbox'] li"));
    }
}