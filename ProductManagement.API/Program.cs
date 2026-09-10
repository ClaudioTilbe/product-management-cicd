using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Services;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Extensions;
using ProductManagement.Infrastructure.Repositories;



var builder = WebApplication.CreateBuilder(args);



#region Configuration

var environmentFile = $"appsettings.{builder.Environment.EnvironmentName}.json";

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(environmentFile, optional: true, reloadOnChange: true)
    .AddEnvironmentVariables(); // Docker y CI/CD pueden sobreescribir cualquier valor



// CAMBIO: Production separado de Staging
// Development / Testing / UI → trabajo local sin Docker
// Staging                    → Docker local y CI/CD
// Production                 → servidor real
var validEnvironments = new[]
{
    "Development",
    "Testing",
    "Staging",
    "Production"
};

if (!validEnvironments.Contains(builder.Environment.EnvironmentName))
{
    throw new InvalidOperationException(
        $"Invalid environment: {builder.Environment.EnvironmentName}"
    );
}


#endregion




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health check para que el pipeline y Docker sepan cuándo la API está lista
builder.Services.AddHealthChecks();

#region Database

var connectionString =
    builder.Configuration["ConnectionStrings:DefaultConnection"];

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string is missing."
    );
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

#endregion

#region CORS

// CAMBIO: solo Production restringe orígenes
// Staging usa AllowAnyOrigin igual que Development — útil para verificar en CI
if (builder.Environment.IsProduction())
{
    var allowedOrigins = builder.Configuration
        .GetSection("AllowedOrigins")
        .Get<string[]>() ?? [];

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AngularPolicy", policy =>
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
        );
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AngularPolicy", policy =>
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
        );
    });
}

#endregion

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

#region Database Initialization

var databaseEnvironments = new[]
{
    "Development",
    "Testing",
    "Staging",
    "Production"
};

if (databaseEnvironments.Contains(app.Environment.EnvironmentName))
{
    // CAMBIO: solo Production no seedea — Staging sí tiene datos de prueba
    await app.Services.InitializeDatabase(seedData: !app.Environment.IsProduction()
    );
}

#endregion



#region Swagger

// CAMBIO: Swagger activo en Development, Testing, UI y Staging
// Solo se deshabilita en Production
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion



//Docker nginx maneja HTTPS externamente (se quita esta linea)
//app.UseHttpsRedirection();

app.UseCors("AngularPolicy");
app.UseAuthorization();

app.MapControllers();

// Endpoint de health check
app.MapHealthChecks("/health");

app.Run();




