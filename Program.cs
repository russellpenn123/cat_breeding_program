using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add Azure App Service logging integration (detects if running in Azure and writes to Log Stream / blobs)
builder.Logging.AddAzureWebAppDiagnostics();

// Add Application Insights telemetry (picks up connection string from configuration or env var APPLICATIONINSIGHTS_CONNECTION_STRING)
builder.Services.AddApplicationInsightsTelemetry();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// (Optional) Add simple startup log entry
app.Logger.LogInformation("CatBreedingProgram started in {Environment} at {UtcNow}", app.Environment.EnvironmentName, DateTime.UtcNow);
app.MapControllers();

app.Run();