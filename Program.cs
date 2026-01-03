using Azure.Storage.Blobs;
using CatBreedingProgram.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add Azure App Service logging integration (detects if running in Azure and writes to Log Stream / blobs)
builder.Logging.AddAzureWebAppDiagnostics();

// Add Application Insights telemetry (picks up connection string from configuration or env var APPLICATIONINSIGHTS_CONNECTION_STRING)
builder.Services.AddApplicationInsightsTelemetry();

// Configure Cosmos DB
var cosmosEndpoint = builder.Configuration["CosmosDb:EndpointUri"];
var cosmosPrimaryKey = builder.Configuration["CosmosDb:PrimaryKey"];

builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Initializing Cosmos DB client for endpoint: {Endpoint}", cosmosEndpoint);
    
    return new CosmosClient(cosmosEndpoint, cosmosPrimaryKey, new CosmosClientOptions
    {
        SerializerOptions = new CosmosSerializationOptions
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
    });
});


var storageConnectionString = builder.Configuration.GetConnectionString("Storage")
    ?? throw new InvalidOperationException("Missing ConnectionStrings:Storage");

builder.Services.AddSingleton(_ => new BlobServiceClient(storageConnectionString));

// Register repositories
builder.Services.AddScoped<ICatRepository, CatRepository>();
builder.Services.AddScoped<IBehaviourService, BehaviourService>();
builder.Services.AddSingleton<IBehaviourLogStore, BehaviourLogStore>();

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