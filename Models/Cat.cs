using System.ComponentModel.DataAnnotations;

namespace CatBreedingProgram.Models;

public class Cat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public bool IsMale { get; set; }
    
    [RegularExpression(@"^#[0-9A-Fa-f]{6}$", ErrorMessage = "Fur colour must be a valid hex code (e.g., #FF5733)")]
    public string FurColourHex { get; set; } = string.Empty;
}