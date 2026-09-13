namespace WeatherActivityPlanner.Models;

public class FavoriteLocation
{
    public int Id { get; set; }

    public required string LocationId { get; set; }

    public required string Name { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static FavoriteLocation CreateFrom(WeatherLocation location, DateTimeOffset createdAt)
    {
        return new FavoriteLocation
        {
            LocationId = location.Id,
            Name = location.Name,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            CreatedAt = createdAt
        };
    }
}
