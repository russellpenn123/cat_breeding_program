using CatBreedingProgram.Models;

namespace CatBreedingProgram.Services;
public class CatRepository : ICatRepository
{
    public Task<Cat> AddCatAsync(Cat cat)
    {
        int newId = new Random().Next(1, 1000);
        cat.Id = newId;
        return Task.FromResult(cat);
    }
}