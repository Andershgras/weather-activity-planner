namespace WeatherActivityPlanner.Models;

public class WeatherSnapshot
{
    public required string LocationName { get; init; }

    public required DateTimeOffset ObservedAt { get; init; }

    public required double TemperatureCelsius { get; init; }

    public required double WindSpeedKmh { get; init; }

    public required double PrecipitationMm { get; init; }

    public required double RainMm { get; init; }

    public bool HasRain => RainMm > 0 || PrecipitationMm > 0;
}
