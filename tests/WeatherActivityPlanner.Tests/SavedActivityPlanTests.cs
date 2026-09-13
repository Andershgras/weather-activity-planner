using WeatherActivityPlanner.Models;

namespace WeatherActivityPlanner.Tests;

public class SavedActivityPlanTests
{
    [Fact]
    public void CreateFrom_CopiesWeatherAndSuggestionIntoSavedPlan()
    {
        var observedAt = new DateTimeOffset(2026, 9, 13, 14, 30, 0, TimeSpan.Zero);
        var createdAt = new DateTimeOffset(2026, 9, 13, 15, 0, 0, TimeSpan.Zero);
        var weather = new WeatherSnapshot
        {
            LocationName = "Copenhagen, Denmark",
            ObservedAt = observedAt,
            TemperatureCelsius = 18.5,
            WindSpeedKmh = 12.4,
            PrecipitationMm = 0.1,
            RainMm = 0
        };
        var suggestion = new ActivitySuggestion
        {
            Title = "Walking",
            Description = "Mild and dry weather makes this a good time for a walk.",
            Severity = "positive"
        };

        var savedPlan = SavedActivityPlan.CreateFrom(weather, suggestion, createdAt);

        Assert.Equal("Copenhagen, Denmark", savedPlan.LocationName);
        Assert.Equal(observedAt, savedPlan.ObservedAt);
        Assert.Equal(18.5, savedPlan.TemperatureCelsius);
        Assert.Equal(12.4, savedPlan.WindSpeedKmh);
        Assert.Equal(0.1, savedPlan.PrecipitationMm);
        Assert.Equal("Walking", savedPlan.SuggestionTitle);
        Assert.Equal("Mild and dry weather makes this a good time for a walk.", savedPlan.SuggestionDescription);
        Assert.Equal("positive", savedPlan.SuggestionSeverity);
        Assert.Equal(createdAt, savedPlan.CreatedAt);
    }
}
