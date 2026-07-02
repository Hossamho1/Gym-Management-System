
using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace GymRoute.Presentation.Data.Seeder;

public static class PlanSeeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();
        if (!await dbContext.Plans.AnyAsync())
        {
            var currentTime = DateTime.UtcNow;

            var plans = new List<Plan>
            {
                new Plan
                {
                    Name = "Monthly Basic",
                    Description = "Full access to gym floor and cardio equipment.",
                    DurationDays = 30,
                    Price = 29.99m,
                    IsActive = true,
                    CreatedAt = currentTime,
                    UpdatedAt = currentTime
                },
                new Plan
                {
                    Name = "3-Month Premium",
                    Description = "Includes pool access, group fitness classes, and 1 free PT session.",
                    DurationDays = 90,
                    Price = 79.99m,
                    IsActive = true,
                    CreatedAt = currentTime,
                    UpdatedAt = currentTime
                },
                new Plan
                {
                    Name = "Old Weekend Only",
                    Description = "Legacy plan - Weekend access only.",
                    DurationDays = 30,
                    Price = 15.00m,
                    IsActive = false,
                    CreatedAt = currentTime.AddMonths(-6), // Created 6 months ago
                   UpdatedAt = currentTime
                }

            };

            await dbContext.Plans.AddRangeAsync(plans);

            await dbContext.SaveChangesAsync();
        }
    }
}