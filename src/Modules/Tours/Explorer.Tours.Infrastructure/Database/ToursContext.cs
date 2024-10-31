using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.TourExecutions;
using Microsoft.EntityFrameworkCore;
using TourObject = Explorer.Tours.Core.Domain.TourObject;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourEquipment> TourEquipment { get; set; }
    public DbSet<TourReview> TourReview { get; set; }
    public DbSet<KeyPoint> KeyPoints { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourObject> TourObjects { get; set; }
    public DbSet<TourExecution>  TourExecutions { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        modelBuilder.Entity<Tour>().
            HasMany(t => t.KeyPoints)
            .WithOne();

        modelBuilder.Entity<Tour>().
            HasMany(t => t.Equipments)
            .WithMany();

        modelBuilder.Entity<Tour>().Property(item => item.TourDurations).HasColumnType("jsonb");
        modelBuilder.Entity<TourExecution>().Property(item => item.CompletedKeyPoints).HasColumnType("jsonb");

    }

   
}