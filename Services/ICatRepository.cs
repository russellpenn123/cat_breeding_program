using CatBreedingProgram.Models;

namespace CatBreedingProgram.Services;

public interface ICatRepository
{
    Task<Cat> AddCatAsync(Cat cat);

    Task<List<Cat>> GetAllCatsAsync();

    Task<Cat> GetCatByName(string name);
}
