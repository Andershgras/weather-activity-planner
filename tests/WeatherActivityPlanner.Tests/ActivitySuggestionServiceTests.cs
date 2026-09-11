using WeatherActivityPlanner.Models;
using WeatherActivityPlanner.Services;

namespace WeatherActivityPlanner.Tests;

public class ActivitySuggestionServiceTests
{
    private readonly ActivitySuggestionService _service = new();

    [Fact]
    public void GetSuggestion_WhenRainIsDetected_ReturnsRainWarning()
    {
        var weather = CreateWeather(temperatureCelsius: 16, windSpeedKmh: 12, precipitationMm: 0.2);

        var suggestion = _service.GetSuggestion(weather);

        Assert.Equal("Rain warning", suggestion.Title);
        Assert.Equal("warning", suggestion.Severity);
    }

    [Fact]
    public void GetSuggestion_WhenWeatherIsCold_ReturnsIndoorActivity()
    {
        var weather = CreateWeather(temperatureCelsius: 4, windSpeedKmh: 10);

        var suggestion = _service.GetSuggestion(weather);

        Assert.Equal("Indoor activity", suggestion.Title);
        Assert.Equal("neutral", suggestion.Severity);
    }

    [Fact]
    public void GetSuggestion_WhenWeatherIsVeryWindy_ReturnsIndoorActivity()
    {
        var weather = CreateWeather(temperatureCelsius: 14, windSpeedKmh: 38);

        var suggestion = _service.GetSuggestion(weather);

        Assert.Equal("Indoor activity", suggestion.Title);
        Assert.Equal("neutral", suggestion.Severity);
    }

    [Fact]
    public void GetSuggestion_WhenWeatherIsMildAndDry_ReturnsWalking()
    {
        var weather = CreateWeather(temperatureCelsius: 18, windSpeedKmh: 15);

        var suggestion = _service.GetSuggestion(weather);

        Assert.Equal("Walking", suggestion.Title);
        Assert.Equal("positive", suggestion.Severity);
    }

    [Fact]
    public void GetSuggestion_WhenWeatherIsDryAndWindIsManageableOutsideMildRange_ReturnsCycling()
    {
        var weather = CreateWeather(temperatureCelsius: 24, windSpeedKmh: 20);

        var suggestion = _service.GetSuggestion(weather);

        Assert.Equal("Cycling", suggestion.Title);
        Assert.Equal("positive", suggestion.Severity);
    }

    private static WeatherSnapshot CreateWeather(
        double temperatureCelsius,
        double windSpeedKmh,
        double precipitationMm = 0,
        double rainMm = 0)
    {
        return new WeatherSnapshot
        {
            LocationName = "Test location",
            ObservedAt = new DateTimeOffset(2026, 9, 11, 12, 0, 0, TimeSpan.Zero),
            TemperatureCelsius = temperatureCelsius,
            WindSpeedKmh = windSpeedKmh,
            PrecipitationMm = precipitationMm,
            RainMm = rainMm
        };
    }
}
