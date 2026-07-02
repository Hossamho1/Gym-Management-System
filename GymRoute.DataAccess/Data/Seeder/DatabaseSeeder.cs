using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Data.Seeder;
using GymRoute.Presentation.Data.Seeder;
namespace GymRoute.DataAccess.Data.Seeder;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(GymDbContext dbContext)
    {
        await PlanSeeder.SeedAsync(dbContext);
        await Categoryseeder.SeedAsync(dbContext);
    }
    

}
