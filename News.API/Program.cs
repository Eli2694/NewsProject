using Logger;
using Microsoft.EntityFrameworkCore;
using News.DAL;
using News.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("UniqeIdsScanner_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

// Get connection string
var connectionString = GetConnectionString(config);


// Add DbContext with the configured connection string
builder.Services.AddSingleton<LogManager>();
builder.Services.AddDbContext<DataLayer>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<MainManager>();


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Function to get connection string
static string GetConnectionString(IConfiguration config)
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

    if (dbHost == null || dbName == null || dbPassword == null)
    {
        // If any of the environment variables were not found, return the connection string from appsettings
        return config.GetConnectionString("DefaultConnection");
    }

    return $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=true";
}