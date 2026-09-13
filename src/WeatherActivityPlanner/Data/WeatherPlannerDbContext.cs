using Microsoft.EntityFrameworkCore;
using WeatherActivityPlanner.Models;

namespace WeatherActivityPlanner.Data;

public class WeatherPlannerDbContext : DbContext
{
    public WeatherPlannerDbContext(DbContextOptions<WeatherPlannerDbContext> options)
        : base(options)
    {
    }

    public DbSet<SavedActivityPlan> SavedActivityPlans => Set<SavedActivityPlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SavedActivityPlan>(entity =>
        {
            entity.Property(plan => plan.LocationName).HasMaxLength(120);
            entity.Property(plan => plan.SuggestionTitle).HasMaxLength(80);
            entity.Property(plan => plan.SuggestionDescription).HasMaxLength(300);
            entity.Property(plan => plan.SuggestionSeverity).HasMaxLength(30);
        });
    }
}
