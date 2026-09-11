using WeatherActivityPlanner.Models;

namespace WeatherActivityPlanner.Services;

public class ActivitySuggestionService
{
    public ActivitySuggestion GetSuggestion(WeatherSnapshot weather)
    {
        if (weather.HasRain)
        {
            return new ActivitySuggestion
            {
                Title = "Rain warning",
                Description = "Choose an indoor activity or bring rain gear if you need to go outside.",
                Severity = "warning"
            };
        }

        if (weather.TemperatureCelsius < 8 || weather.WindSpeedKmh > 35)
        {
            return new ActivitySuggestion
            {
                Title = "Indoor activity",
                Description = "The weather is cold or windy, so an indoor plan is the safest choice.",
                Severity = "neutral"
            };
        }

        if (weather.TemperatureCelsius >= 10 && weather.TemperatureCelsius <= 22)
        {
            return new ActivitySuggestion
            {
                Title = "Walking",
                Description = "Mild and dry weather makes this a good time for a walk.",
                Severity = "positive"
            };
        }

        if (weather.WindSpeedKmh <= 25)
        {
            return new ActivitySuggestion
            {
                Title = "Cycling",
                Description = "Dry weather with manageable wind can work well for cycling.",
                Severity = "positive"
            };
        }

        return new ActivitySuggestion
        {
            Title = "Indoor activity",
            Description = "The conditions are not ideal for outdoor activities right now.",
            Severity = "neutral"
        };
    }
}
