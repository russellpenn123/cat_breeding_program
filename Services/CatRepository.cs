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
            _logger.LogInformation("PK header value: {Pk}", cat.PartitionKey);
            _logger.LogInformation("Cat JSON system json: {Json}", System.Text.Json.JsonSerializer.Serialize(cat));
            _logger.LogInformation("Cat JSON newtonsoft json: {Json}", Newtonsoft.Json.JsonConvert.SerializeObject(cat));

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

    public async Task<List<Cat>> GetAllCatsAsync()
    {
        var cats = new List<Cat>();
        var query = "SELECT * FROM c";
        var queryDefinition = new QueryDefinition(query);
        var feedIterator = _container.GetItemQueryIterator<Cat>(queryDefinition);

        while (feedIterator.HasMoreResults)
        {
            var response = await feedIterator.ReadNextAsync();
            cats.AddRange(response.Resource);
        }

        return cats;
    }
}