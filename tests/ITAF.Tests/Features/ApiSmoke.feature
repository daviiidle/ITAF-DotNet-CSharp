@api @smoke
Feature: API smoke checks
  As a QA automation engineer
  I want a reusable API automation foundation
  So that service scenarios can be expressed with Gherkin

  Scenario: Existing post can be retrieved
    When I request post 1
    Then the API response should be successful
    And the post response should have id 1

  Scenario: New post payload can be submitted
    Given I have a generated post payload
    When I submit the generated post
    Then the API response should be successful
    And the created post should include the submitted title

