# ITAF-DotNet-CSharp

Integrated Test Automation Framework built with .NET, C#, Reqnroll BDD/Gherkin, NUnit, Playwright for .NET, API automation, reusable framework utilities, reporting hooks, and GitHub Actions CI.

## Tech Stack

- .NET 8
- C#
- Reqnroll for BDD and Gherkin
- NUnit test runner
- Playwright for .NET for UI automation
- HttpClient-based API automation
- Bogus for test data
- FluentAssertions for readable assertions
- Serilog for logging
- GitHub Actions for CI

## Structure

```text
ITAF-DotNet-CSharp/
  src/
    ITAF.Core/
    ITAF.UI/
    ITAF.API/
  tests/
    ITAF.Tests/
      Features/
      StepDefinitions/
      Hooks/
  config/
  docs/
  .github/workflows/
```

## Prerequisites

- .NET 8 SDK
- PowerShell Core, required by the Playwright browser install script

## Setup

```bash
dotnet restore ITAF-DotNet-CSharp.sln
dotnet build ITAF-DotNet-CSharp.sln
pwsh tests/ITAF.Tests/bin/Debug/net8.0/playwright.ps1 install chromium
```

## Run Tests

Run all tests:

```bash
dotnet test ITAF-DotNet-CSharp.sln
```

Run smoke tests:

```bash
dotnet test ITAF-DotNet-CSharp.sln --filter "Category=smoke"
```

Run UI tests only:

```bash
dotnet test ITAF-DotNet-CSharp.sln --filter "Category=ui"
```

Run API tests only:

```bash
dotnet test ITAF-DotNet-CSharp.sln --filter "Category=api"
```

Run with local visible browser settings:

```bash
ITAF_ENVIRONMENT=local dotnet test ITAF-DotNet-CSharp.sln --filter "Category=ui"
```

## Example Gherkin

```gherkin
@ui @smoke
Feature: UI smoke checks

  Scenario: Documentation home page loads
    Given I open the configured UI application
    Then the documentation home page should be displayed
    And the page title should contain "Playwright"
```

## Configuration

Default settings are in `config/appsettings.json`. Local overrides are in `config/appsettings.local.json`.

Environment variables can override configuration by using the `ITAF_` prefix.

## GitHub Repo Setup

After creating the remote repo on GitHub:

```bash
git remote add origin https://github.com/daviiidle/ITAF-DotNet-CSharp.git
git branch -M main
git push -u origin main
```

If GitHub CLI is installed and authenticated:

```bash
gh repo create daviiidle/ITAF-DotNet-CSharp --public --source=. --remote=origin --push
```

