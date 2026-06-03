@ui @smoke
Feature: UI smoke checks
  As a QA automation engineer
  I want a reusable UI automation foundation
  So that browser scenarios can be expressed with Gherkin

  Scenario: Documentation home page loads
    Given I open the configured UI application
    Then the documentation home page should be displayed
    And the page title should contain "Playwright"

