using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using SeleniumFramework.Pages;

namespace SeleniumFramework.StepDefinitions;

[Binding]
public class GoogleSteps
{
    private readonly GoogleHomePage _googlePage;

    public GoogleSteps(ScenarioContext scenarioContext)
    {
        var driver = (IWebDriver)scenarioContext["driver"];
        _googlePage = new GoogleHomePage(driver);
    }

    [Given("I am on the Google homepage")]
    public void GivenIAmOnTheGoogleHomepage()
    {
        // Already navigated in Hook
    }

    [When("""I enter "(.*)" into the search box""")]
    public void WhenIEnterText(string query)
    {
        _googlePage.EnterSearchText(query);
    }

    [Then("I should see search suggestions")]
    public void ThenIShouldSeeSearchSuggestions()
    {
        var suggestions = _googlePage.GetSearchSuggestions();
        Assert.That(suggestions, Is.Not.Null);
    }
}