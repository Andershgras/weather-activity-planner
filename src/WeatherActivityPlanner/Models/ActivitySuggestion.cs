namespace WeatherActivityPlanner.Models;

public class ActivitySuggestion
{
    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string Severity { get; init; }
}
