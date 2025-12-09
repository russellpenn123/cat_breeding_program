using CatBreedingProgram.Models;

namespace CatBreedingProgram.Services;

public interface ICatRepository
{
    Task<Cat> AddCatAsync(Cat cat);
}
