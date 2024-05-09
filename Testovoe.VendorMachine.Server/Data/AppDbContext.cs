using Microsoft.EntityFrameworkCore;
using Testovoe.VendorMachine.Server.Models;

namespace Testovoe.VendorMachine.Server.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Coin> Coins { get; set; }
    public DbSet<Soda> Sodas { get; set; }

    public async Task SeedLargeData()
    {
        if (Sodas.Count() < 1)
        {
            await Sodas.AddRangeAsync(
                new Soda { Count = 6, Price = 49, Image = Resources.SodaImages.Coke },
                new Soda { Count = 7, Price = 58, Image = Resources.SodaImages.Pepsi },
                new Soda { Count = 10, Price = 69, Image = Resources.SodaImages.Sprite },
                new Soda { Count = 3, Price = 29, Image = Resources.SodaImages.Fanta });
        }

        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coin>().HasData(
            new Coin { Id = 1, Count = 40, Value = 1 },
            new Coin { Id = 2, Count = 30, Value = 2 },
            new Coin { Id = 3, Count = 20, Value = 5, IsBlocked = true },
            new Coin { Id = 4, Count = 10, Value = 10 });
    }
}
