using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManagement.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        // CAMBIO: añade el parámetro seedData
        public static async Task InitializeDatabase(this IServiceProvider services, bool seedData = true)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

            // NUEVO: reintenta hasta 5 veces con 5 segundos entre intentos
            var retries = 5;
            var delay = TimeSpan.FromSeconds(5);

            for (int attempt = 1; attempt <= retries; attempt++)
            {
                try
                {
                    logger.LogInformation(
                        "Applying database migrations (attempt {Attempt}/{Max})...",
                        attempt, retries);

                    await context.Database.MigrateAsync();

                    logger.LogInformation("Migrations applied successfully.");

                    if (seedData)
                        await DatabaseSeeder.SeedAsync(context);

                    return; // éxito — salimos del loop
                }
                catch (Exception ex) when (attempt < retries)
                {
                    logger.LogWarning(
                        "Database not ready (attempt {Attempt}/{Max}). Retrying in {Delay}s... Error: {Message}",
                        attempt, retries, delay.TotalSeconds, ex.Message);

                    await Task.Delay(delay);
                }
                catch (Exception ex)
                {
                    // último intento fallido — lanza para que el contenedor muera con error claro
                    logger.LogError(ex, "An error occurred initializing the database after {Max} attempts.", retries);
                    throw;
                }
            }
        }
    }


}
