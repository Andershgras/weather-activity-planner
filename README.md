# Weather Activity Planner

Weather Activity Planner is a portfolio project built with ASP.NET Core and Razor Pages.

The goal is to show a clean, practical integration with a free third-party weather API. The app will use Open-Meteo weather data to suggest simple activities such as walking, running, cycling, indoor activity, or a rain warning.

## MVP Scope

- ASP.NET Core Razor Pages application.
- Open-Meteo API integration for weather data.
- Simple activity suggestions based on weather conditions.
- Clean, serious, and easy-to-understand dashboard UI.
- Database persistence for portfolio-relevant data, such as favorite locations, saved activity plans, or search history.

## Current Status

Initial project structure has been created. The dashboard now fetches and displays current weather for a selected Danish location through Open-Meteo, then shows a basic activity suggestion based on simple weather rules.

Database persistence will be added in a later verified step.

## Project Structure

```text
src/
  WeatherActivityPlanner/
    Pages/
    wwwroot/
    Program.cs
    WeatherActivityPlanner.csproj
tests/
  WeatherActivityPlanner.Tests/
docs/
  ai-development-guide.md
```

## Tests

The test project is located in `tests/WeatherActivityPlanner.Tests`.

Current tests cover the basic activity suggestion rules:

- rain or precipitation shows a rain warning.
- cold weather suggests an indoor activity.
- very windy weather suggests an indoor activity.
- mild and dry weather suggests walking.
- dry weather with manageable wind outside the mild range suggests cycling.

Run all tests from the repository root:

```powershell
dotnet test WeatherActivityPlanner.slnx
```

## Development Principles

This project should be developed in small, verified steps. Keep the code simple, readable, and aligned with standard ASP.NET Core and Razor Pages patterns.

See [docs/ai-development-guide.md](docs/ai-development-guide.md) for the project-specific AI development guidelines.
