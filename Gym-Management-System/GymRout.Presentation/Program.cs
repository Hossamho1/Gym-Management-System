using GymRoute.BusinessLogic;
using GymRoute.DataAccess;
using GymRoute.DataAccess.Data.Contexts;
using GymRoute.DataAccess.Data.Seeder;
using GymRoute.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddDbContext<GymDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not found.");

builder.Services.AddGymDataAccess(connectionString);
builder.Services.AddBusinessLogic();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    try
    {
        var pending = (await dbContext.Database.GetPendingMigrationsAsync()).ToList();
        if (pending.Any())
        {
            logger.LogInformation("Applying {Count} pending EF Core migrations", pending.Count);
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully");
        }

        await DatabaseSeeder.SeedAllAsync(dbContext);
    }
    catch (Exception ex)
    {
        // Log and continue so the app doesn't crash in development when migrations fail.
        logger.LogError(ex, "Database migration or seeding failed. Inspect migration scripts and database dependencies.");
    }

}


app.Run();
