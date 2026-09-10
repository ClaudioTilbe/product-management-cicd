using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.Infrastructure.Data;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace ProductManagement.UITests.Fixtures
{
    public class DatabaseFixture : IDisposable
    {

        public ApplicationDbContext Context { get; }



        public DatabaseFixture()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables() // NUEVO: CI/CD sobreescribe la connection string
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "No se encontró 'DefaultConnection' en appsettings.Test.json. " +
                    "Verifica que el archivo existe y está configurado correctamente."
                );
            }

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            Context = new ApplicationDbContext(options);
        }


        // IDisposable para cerrar la conexión al terminar cada test
        public void Dispose()
        {
            Context?.Dispose();
        }


    }
}
