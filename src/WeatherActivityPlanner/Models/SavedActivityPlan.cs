namespace WeatherActivityPlanner.Models;

public class SavedActivityPlan
{
    public int Id { get; set; }

    public required string LocationName { get; set; }

    public DateTimeOffset ObservedAt { get; set; }

    public double TemperatureCelsius { get; set; }

    public double WindSpeedKmh { get; set; }

    public double PrecipitationMm { get; set; }

    public required string SuggestionTitle { get; set; }

    public required string SuggestionDescription { get; set; }

    public required string SuggestionSeverity { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static SavedActivityPlan CreateFrom(
        WeatherSnapshot weather,
        ActivitySuggestion suggestion,
        DateTimeOffset createdAt)
    {
        return new SavedActivityPlan
        {
            LocationName = weather.LocationName,
            ObservedAt = weather.ObservedAt,
            TemperatureCelsius = weather.TemperatureCelsius,
            WindSpeedKmh = weather.WindSpeedKmh,
            PrecipitationMm = weather.PrecipitationMm,
            SuggestionTitle = suggestion.Title,
            SuggestionDescription = suggestion.Description,
            SuggestionSeverity = suggestion.Severity,
            CreatedAt = createdAt
        };
    }
}
