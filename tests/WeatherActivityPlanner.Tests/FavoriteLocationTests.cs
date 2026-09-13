using WeatherActivityPlanner.Models;

namespace WeatherActivityPlanner.Tests;

public class FavoriteLocationTests
{
    [Fact]
    public void CreateFrom_CopiesWeatherLocationIntoFavoriteLocation()
    {
        var createdAt = new DateTimeOffset(2026, 9, 13, 21, 45, 0, TimeSpan.Zero);
        var location = new WeatherLocation
        {
            Id = "odense",
            Name = "Odense, Denmark",
            Latitude = 55.4038,
            Longitude = 10.4024
        };

        var favoriteLocation = FavoriteLocation.CreateFrom(location, createdAt);

        Assert.Equal("odense", favoriteLocation.LocationId);
        Assert.Equal("Odense, Denmark", favoriteLocation.Name);
        Assert.Equal(55.4038, favoriteLocation.Latitude);
        Assert.Equal(10.4024, favoriteLocation.Longitude);
        Assert.Equal(createdAt, favoriteLocation.CreatedAt);
    }
}
