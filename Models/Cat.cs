using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatBreedingProgram.Models;

public class Cat
{
    // Cosmos DB requires 'id' property (lowercase) as the unique identifier
    [JsonPropertyName("id")]
    public string id { get; set; } = Guid.NewGuid().ToString();

    // Partition key for Cosmos DB - using Name for logical partitioning
    [JsonPropertyName("PartitionKey")]
    public string PartitionKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public bool IsMale { get; set; }
    
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Fur colour must be a valid hex code (e.g., #FF5733)")]
    public string FurColourHex { get; set; } = string.Empty;
}