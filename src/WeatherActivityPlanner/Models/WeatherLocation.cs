namespace WeatherActivityPlanner.Models;

public class WeatherLocation
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required double Latitude { get; init; }

    public required double Longitude { get; init; }
}
