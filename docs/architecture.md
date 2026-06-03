# Architecture

ITAF-DotNet-CSharp is split into small projects so the test layer stays readable and the reusable framework code can grow without becoming tied to one scenario.

## Projects

- `ITAF.Core`: configuration loading, logging, path helpers, and shared framework concerns.
- `ITAF.UI`: Playwright browser lifecycle and page objects.
- `ITAF.API`: API client wrappers, response models, domain models, and test data factories.
- `ITAF.Tests`: Reqnroll feature files, step definitions, hooks, and NUnit execution.

## BDD Flow

Feature files live in `tests/ITAF.Tests/Features`. Reqnroll generates NUnit tests from those `.feature` files during build. Step definitions bind plain-language Gherkin steps to C# methods.

Tags control lifecycle setup:

- `@ui` starts a Playwright browser session before the scenario and closes it afterwards.
- `@api` starts an API client before the scenario and disposes it afterwards.
- `@smoke` identifies scenarios suitable for fast CI validation.

## Configuration

Configuration is loaded from:

1. `appsettings.json`
2. `appsettings.{ITAF_ENVIRONMENT}.json`
3. Environment variables prefixed with `ITAF_`

Use `ITAF_ENVIRONMENT=local` to load `appsettings.local.json`.

## Reporting

The framework writes logs to `reports/itaf-{date}.log` and captures screenshots for failed `@ui` scenarios under `reports/screenshots`.

