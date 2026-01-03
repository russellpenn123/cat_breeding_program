namespace CatBreedingProgram.Models;

public record BehaviourLogRequest
{
    public string CatName { get; set; } = string.Empty;

    public int BehaviourScore { get; set; }

    public string BehaviourDescription { get; set; } = string.Empty;
}