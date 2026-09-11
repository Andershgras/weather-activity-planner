using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherActivityPlanner.Models;
using WeatherActivityPlanner.Services;

namespace WeatherActivityPlanner.Pages;

public class IndexModel : PageModel
{
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

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            CurrentWeather = await _weatherService.GetCurrentWeatherForCopenhagenAsync(cancellationToken);
            ActivitySuggestion = _activitySuggestionService.GetSuggestion(CurrentWeather);
        }
        catch
        {
            WeatherErrorMessage = "Current weather could not be loaded. Please try again later.";
        }
    }
}
