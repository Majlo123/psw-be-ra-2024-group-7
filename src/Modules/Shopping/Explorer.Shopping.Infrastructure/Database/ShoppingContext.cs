using Explorer.Shopping.Core.Domain;
using Explorer.Shopping.Core.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Shopping.Infrastructure.Database;

public class ShoppingContext : DbContext
{
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<TourPurchaseToken> PurchaseTokens { get; set; }
    public DbSet<Item> Items { get; set; }
    public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("shopping");
        modelBuilder.Entity<ShoppingCart>()
           .Property(cart => cart.Items)
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
