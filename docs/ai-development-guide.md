# AI Development Guide

This guide defines how AI assistance should work on Weather Activity Planner.

## Project Scope

Weather Activity Planner is an ASP.NET Core Razor Pages portfolio project. The app uses the free Open-Meteo API to fetch weather data and suggest simple activities based on the weather.

This is not an AI-token-based app. Do not change the core concept without asking first.

## Working Style

- Work in small, verified steps.
- Keep each step focused and understandable.
- Do not add extra features without asking first.
- Prefer the simplest solution that fits the current milestone.
- Preserve existing project structure and style unless there is a clear reason to change it.

## Language

Use English for:

- UI text.
- README content.
- Code comments.
- Project documentation.
- Commit message suggestions.

## ASP.NET Core and Razor Pages

- Prefer clean ASP.NET Core and Razor Pages patterns.
- Keep page models simple and readable.
- Use services for external API access when Open-Meteo integration is added.
- Keep business rules easy to follow.
- Avoid unnecessary abstractions, frameworks, or architectural layers.

## UI Direction

- Keep the UI serious, simple, and clean.
- Build a clear dashboard instead of a marketing-style landing page.
- Prioritize readability and practical workflows.
- Avoid decorative UI that does not support the app's purpose.

## Data Persistence

- Use a database for portfolio-relevant persisted data.
- Good persistence candidates include favorite locations, saved activity plans, or search history.
- Use localStorage only for minor UI preferences if needed.
- Do not use localStorage for important application data.

## Verification

Verify relevant changes with the smallest useful checks, such as:

- `dotnet build`
- Unit or integration tests when meaningful.
- Manual or automated smoke checks for user-facing behavior.

Build success alone does not prove API, database, or browser behavior. Use the most relevant check for the change being made.

## Step Completion

After each completed step, provide:

- A short summary of what changed.
- What was verified.
- A focused suggested commit message.

Then stop and wait for confirmation before expanding the scope.
