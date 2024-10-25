using Explorer.Shopping.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ShoppingContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }

    public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");
        modelBuilder.Entity<ShoppingCart>()
           .Property(cart => cart.items)
           .HasColumnType("jsonb");
        ConfigureShoppingCart(modelBuilder);
    }

    private static void ConfigureShoppingCart(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TourPurchaseToken>()
            .HasIndex(tpt => new { tpt.UserId, tpt.TourId })
            .IsUnique();
    }
}
