using System.Text.Json;
using System.Text.Json.Serialization;
using WeatherActivityPlanner.Models;

namespace WeatherActivityPlanner.Services;

public class OpenMeteoWeatherService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private readonly HttpClient _httpClient;

    public OpenMeteoWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.open-meteo.com/");
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<WeatherSnapshot> GetCurrentWeatherAsync(
        WeatherLocation location,
        CancellationToken cancellationToken)
    {
        var requestUri = FormattableString.Invariant(
            $"v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&current=temperature_2m,precipitation,rain,wind_speed_10m&timezone=auto");

        using var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var forecast = await JsonSerializer.DeserializeAsync<OpenMeteoForecastResponse>(
            responseStream,
            JsonSerializerOptions,
            cancellationToken);

        if (forecast?.Current is null)
        {
            throw new InvalidOperationException("Open-Meteo did not return current weather data.");
        }

        return new WeatherSnapshot
        {
            LocationName = location.Name,
            ObservedAt = DateTimeOffset.Parse(forecast.Current.Time),
            TemperatureCelsius = forecast.Current.Temperature2m,
            WindSpeedKmh = forecast.Current.WindSpeed10m,
            PrecipitationMm = forecast.Current.Precipitation,
            RainMm = forecast.Current.Rain
        };
    }

    private sealed class OpenMeteoForecastResponse
    {
        public OpenMeteoCurrentWeather? Current { get; set; }
    }

    private sealed class OpenMeteoCurrentWeather
    {
        public required string Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public required double Temperature2m { get; set; }

        public required double Precipitation { get; set; }

        public required double Rain { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public required double WindSpeed10m { get; set; }
    }
}
