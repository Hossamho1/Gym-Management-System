using GymRoute.DataAccess.Entities;
using GymRoute.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;


namespace GymRoute.DataAccess.Data.Contexts;


public class GymDbContext : DbContext
{


    public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);
    }
    public DbSet<Plan> Plans { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<GymUser> Users { get; set; } = default!;
   public  DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    public DbSet<Booking> Bookings { get; set; } = default!;
    public DbSet<Session> Sessions { get; set; } = default!;
    public DbSet<Trainer> Trainers { get; set; } = default!;
    public DbSet<Member> Members { get; set; } = default!;



}
