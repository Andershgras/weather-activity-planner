using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WeatherActivityPlanner.Data;
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
    private readonly WeatherPlannerDbContext _dbContext;

    public IndexModel(
        OpenMeteoWeatherService weatherService,
        ActivitySuggestionService activitySuggestionService,
        WeatherPlannerDbContext dbContext)
    {
        _weatherService = weatherService;
        _activitySuggestionService = activitySuggestionService;
        _dbContext = dbContext;
    }

    public WeatherSnapshot? CurrentWeather { get; private set; }

    public ActivitySuggestion? ActivitySuggestion { get; private set; }

    public IReadOnlyList<SavedActivityPlan> SavedActivityPlans { get; private set; } = [];

    public string? WeatherErrorMessage { get; private set; }

    public string? SavedDataErrorMessage { get; private set; }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SelectedLocationId { get; set; } = "copenhagen";

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        await LoadSavedActivityPlansAsync(cancellationToken);
        await LoadCurrentWeatherAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostSavePlanAsync(CancellationToken cancellationToken)
    {
        await LoadCurrentWeatherAsync(cancellationToken);

        if (CurrentWeather is null || ActivitySuggestion is null)
        {
            await LoadSavedActivityPlansAsync(cancellationToken);
            WeatherErrorMessage = "The activity plan could not be saved because current weather could not be loaded.";
            return Page();
        }

        var savedPlan = SavedActivityPlan.CreateFrom(
            CurrentWeather,
            ActivitySuggestion,
            DateTimeOffset.Now);

        try
        {
            var alreadySaved = await _dbContext.SavedActivityPlans.AnyAsync(
                plan => plan.LocationName == savedPlan.LocationName
                    && plan.ObservedAt == savedPlan.ObservedAt
                    && plan.SuggestionTitle == savedPlan.SuggestionTitle,
                cancellationToken);

            if (alreadySaved)
            {
                StatusMessage = "This activity plan is already saved.";
                return RedirectToPage(new { selectedLocationId = SelectedLocationId });
            }

            _dbContext.SavedActivityPlans.Add(savedPlan);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await LoadSavedActivityPlansAsync(cancellationToken);
            SavedDataErrorMessage = "The activity plan could not be saved because the database could not be reached.";
            return Page();
        }

        StatusMessage = "Activity plan saved.";

        return RedirectToPage(new { selectedLocationId = SelectedLocationId });
    }

    public async Task<IActionResult> OnPostDeletePlanAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var savedPlan = await _dbContext.SavedActivityPlans
                .FirstOrDefaultAsync(plan => plan.Id == id, cancellationToken);

            if (savedPlan is not null)
            {
                _dbContext.SavedActivityPlans.Remove(savedPlan);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        catch
        {
            await LoadCurrentWeatherAsync(cancellationToken);
            await LoadSavedActivityPlansAsync(cancellationToken);
            SavedDataErrorMessage = "The activity plan could not be deleted because the database could not be reached.";
            return Page();
        }

        StatusMessage = "Activity plan deleted.";

        return RedirectToPage(new { selectedLocationId = SelectedLocationId });
    }

    public async Task<IActionResult> OnPostClearPlansAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.SavedActivityPlans.ExecuteDeleteAsync(cancellationToken);
        }
        catch
        {
            await LoadCurrentWeatherAsync(cancellationToken);
            await LoadSavedActivityPlansAsync(cancellationToken);
            SavedDataErrorMessage = "Saved activity plans could not be cleared because the database could not be reached.";
            return Page();
        }

        StatusMessage = "Saved activity plans cleared.";

        return RedirectToPage(new { selectedLocationId = SelectedLocationId });
    }

    private async Task LoadCurrentWeatherAsync(CancellationToken cancellationToken)
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

    private async Task LoadSavedActivityPlansAsync(CancellationToken cancellationToken)
    {
        try
        {
            SavedActivityPlans = await _dbContext.SavedActivityPlans
                .OrderByDescending(plan => plan.CreatedAt)
                .Take(5)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        catch
        {
            SavedActivityPlans = [];
            SavedDataErrorMessage = "Saved activity plans could not be loaded.";
        }
    }
}
