namespace CatBreedingProgram.Services;

public interface IBehaviourLogStore
{
     Task SaveTextAsync(string blobName, string content);  
}