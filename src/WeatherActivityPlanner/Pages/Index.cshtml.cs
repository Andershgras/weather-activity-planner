using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherActivityPlanner.Models;
using WeatherActivityPlanner.Services;

namespace WeatherActivityPlanner.Pages;

public class IndexModel : PageModel
{
    private readonly OpenMeteoWeatherService _weatherService;

    public IndexModel(OpenMeteoWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public WeatherSnapshot? CurrentWeather { get; private set; }

    public string? WeatherErrorMessage { get; private set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        try
        {
            CurrentWeather = await _weatherService.GetCurrentWeatherForCopenhagenAsync(cancellationToken);
        }
        catch
        {
            WeatherErrorMessage = "Current weather could not be loaded. Please try again later.";
        }
    }
}
