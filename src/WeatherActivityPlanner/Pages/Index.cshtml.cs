using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherActivityPlanner.Models;
using WeatherActivityPlanner.Services;

namespace WeatherActivityPlanner.Pages;

public class IndexModel : PageModel
{
    public static readonly IReadOnlyList<WeatherLocation> AvailableLocations =
    [
        new()
        {
            Id = "copenhagen",
            Name = "Copenhagen, Denmark",
            Latitude = 55.6761,
            Longitude = 12.5683
        },
        new()
        {
            Id = "aarhus",
            Name = "Aarhus, Denmark",
            Latitude = 56.1629,
            Longitude = 10.2039
        },
        new()
        {
            Id = "odense",
            Name = "Odense, Denmark",
            Latitude = 55.4038,
            Longitude = 10.4024
        }
    ];

    private readonly OpenMeteoWeatherService _weatherService;
    private readonly ActivitySuggestionService _activitySuggestionService;

    public IndexModel(
        OpenMeteoWeatherService weatherService,
        ActivitySuggestionService activitySuggestionService)
    {
        _weatherService = weatherService;
        _activitySuggestionService = activitySuggestionService;
    }

    public WeatherSnapshot? CurrentWeather { get; private set; }

    public ActivitySuggestion? ActivitySuggestion { get; private set; }

    public string? WeatherErrorMessage { get; private set; }

    [BindProperty(SupportsGet = true)]
    public string? SelectedLocationId { get; set; } = "copenhagen";

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var selectedLocation = AvailableLocations.FirstOrDefault(location => location.Id == SelectedLocationId)
            ?? AvailableLocations[0];

        SelectedLocationId = selectedLocation.Id;

        try
        {
            CurrentWeather = await _weatherService.GetCurrentWeatherAsync(selectedLocation, cancellationToken);
            ActivitySuggestion = _activitySuggestionService.GetSuggestion(CurrentWeather);
        }
        catch
        {
            WeatherErrorMessage = "Current weather could not be loaded. Please try again later.";
        }
    }
}
