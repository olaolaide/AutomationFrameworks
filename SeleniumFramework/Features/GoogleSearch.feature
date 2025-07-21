Feature: Google Search

    Scenario: Search for SpecFlow on Google
        Given I am on the Google homepage
        When I enter "SpecFlow" into the search box
        Then I should see search suggestions