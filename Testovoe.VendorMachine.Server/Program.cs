
using Microsoft.EntityFrameworkCore;
using Testovoe.VendorMachine.Server.Data;
using Testovoe.VendorMachine.Server.Services;

namespace Testovoe.VendorMachine.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(dbBuilder =>
            dbBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ITokenAuthenticator, TokenAuthenticator>();
        builder.Services.AddScoped<ISodaService, SodaService>();
        builder.Services.AddScoped<ICoinService, CoinService>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            using IServiceScope scope = app.Services.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            logger.LogInformation("Checking migrations...");
            if ((await appDbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                logger.LogInformation("Migrating database...");
                await appDbContext.Database.MigrateAsync();
            }

            logger.LogInformation("Seeding large data...");
            await appDbContext.SeedLargeData();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        await app.RunAsync();
    }
}
