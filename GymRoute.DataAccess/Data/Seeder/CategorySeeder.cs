
using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace GymRoute.Presentation.Data.Seeder;

public static class Categoryseeder
{
    public static async Task SeedAsync(GymDbContext dbContext)
    {
        if (!await dbContext.Categories.AnyAsync())
        {
            var currentTime = DateTime.UtcNow;

            var Categories = new List<Category>
            {
                new Category
                {
                    Name = "Cardio",
                 
                },
                  new Category
                {
                    Name = "Yoga",

                },
                    new Category
                {
                    Name = "Boxing",

                },



            };

            await dbContext.Categories.AddRangeAsync(Categories);

            await dbContext.SaveChangesAsync();
        }
    }
}