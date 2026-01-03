using CatBreedingProgram.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CatBreedingProgram.Services;

public class CatRepository : ICatRepository
{
    private readonly Container _container;
    private readonly ILogger<CatRepository> _logger;

    public CatRepository(CosmosClient cosmosClient, IConfiguration configuration, ILogger<CatRepository> logger)
    {
        _logger = logger;
        
        // Get reference to database and container from configuration
        var databaseName = configuration["CosmosDb:DatabaseName"];
        var containerName = configuration["CosmosDb:ContainerName"];
        
        var database = cosmosClient.GetDatabase(databaseName);
        _container = database.GetContainer(containerName);
    }

    public async Task<Cat> AddCatAsync(Cat cat)
    {
        try
        {
            // Set partition key value
            cat.PartitionKey = cat.Name;
            
            // Ensure we have a unique id
            if (string.IsNullOrEmpty(cat.id))
            {
                cat.id = Guid.NewGuid().ToString();
            }

            _logger.LogInformation("Saving cat {Name} with ID {Id} to Cosmos DB", cat.Name, cat.id);

            // Create item in Cosmos DB
            var response = await _container.CreateItemAsync(cat, new PartitionKey(cat.PartitionKey));

            _logger.LogInformation("Successfully saved cat {Name}. Request charge: {RequestCharge} RU", 
                cat.Name, response.RequestCharge);

            return response.Resource;
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "Cosmos DB error while saving cat {Name}: {StatusCode} - {Message}", 
                cat.Name, ex.StatusCode, ex.Message);
            throw;
        }
    }
}