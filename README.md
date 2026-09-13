# Weather Activity Planner

Weather Activity Planner is a portfolio project built with ASP.NET Core and Razor Pages.

The goal is to show a clean, practical integration with a free third-party weather API. The app uses Open-Meteo weather data to suggest simple activities such as walking, running, cycling, indoor activity, or a rain warning.

## MVP Scope

- ASP.NET Core Razor Pages application.
- Open-Meteo API integration for weather data.
- Simple activity suggestions based on weather conditions.
- Clean, serious, and easy-to-understand dashboard UI.
- Database persistence for portfolio-relevant data, such as favorite locations, saved activity plans, or search history.

## Current Status

The dashboard fetches and displays current weather for a selected Danish location through Open-Meteo, then shows a basic activity suggestion based on simple weather rules.

Saved activity plans and favorite locations are persisted in a SQL Server LocalDB database named `WeatherPlannerDb`. Users can save the current activity plan, see the most recent saved plans, delete individual saved plans, clear all saved plans, and avoid saving duplicate plans for the same weather observation. Users can also save favorite locations, see them sorted at the top of the location selector, and remove a selected location from favorites.

## Implemented Features

- Current weather dashboard for Copenhagen, Aarhus, and Odense.
- Open-Meteo integration for temperature, wind, precipitation, and rain.
- Simple activity suggestion rules based on current weather.
- SQL Server LocalDB persistence for saved activity plans.
- Recent saved plans list with total saved count.
- Duplicate prevention for already saved activity plans.
- Delete one saved plan or clear all saved plans.
- Favorite locations sorted at the top of the location selector.
- Save or remove the selected location as a favorite.
- Clean Razor Pages dashboard UI.

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

## Database

The app uses SQL Server LocalDB for saved activity plans and favorite locations.

- Server: `(localdb)\MSSQLLocalDB`
- Database: `WeatherPlannerDb`

Apply migrations from the repository root:

```powershell
dotnet ef database update --project src\WeatherActivityPlanner\WeatherActivityPlanner.csproj --startup-project src\WeatherActivityPlanner\WeatherActivityPlanner.csproj
```

## Run the App

Run the Razor Pages app from the repository root:

```powershell
dotnet run --project src\WeatherActivityPlanner\WeatherActivityPlanner.csproj
```

The terminal will show the local URL to open in the browser.

## Build

Build the solution from the repository root:

```powershell
dotnet build WeatherActivityPlanner.slnx
```

## Tests

The test project is located in `tests/WeatherActivityPlanner.Tests`.

Current tests cover the basic activity suggestion rules:

- rain or precipitation shows a rain warning.
- cold weather suggests an indoor activity.
- very windy weather suggests an indoor activity.
- mild and dry weather suggests walking.
- dry weather with manageable wind outside the mild range suggests cycling.
- saved activity plans copy the current weather and suggestion into a database-ready snapshot.
- favorite locations copy the selected location into a database-ready snapshot.

Run all tests from the repository root:

```powershell
dotnet test WeatherActivityPlanner.slnx
```

## Development Principles

This project should be developed in small, verified steps. Keep the code simple, readable, and aligned with standard ASP.NET Core and Razor Pages patterns.

See [docs/ai-development-guide.md](docs/ai-development-guide.md) for the project-specific AI development guidelines.
