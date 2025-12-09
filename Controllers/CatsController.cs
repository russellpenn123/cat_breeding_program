using Microsoft.AspNetCore.Mvc;
using CatBreedingProgram.Models;
using CatBreedingProgram.Services;

namespace CatBreedingProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{
    private readonly ILogger<CatsController> _logger;
    private readonly ICatRepository _catRepository;

    public CatsController(ILogger<CatsController> logger, ICatRepository catRepository)
    {
        _logger = logger;
        _catRepository = catRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Cat cat)
    {
        _logger.LogInformation("CatsController invoked - Registering cat: {CatName}, Age: {Age}, Gender: {Gender}, Color: {Color}",
            cat.Name, cat.Age, cat.IsMale ? "Male" : "Female", cat.FurColourHex);

        var savedCat = await _catRepository.AddCatAsync(cat);

        return Ok($"Saved cat: {savedCat.Name} with id {savedCat.Id}. Cute!");
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("CatsContoller invoked - Get all cats");
        return Ok("Returning all registered cats: { mocky moggy }");   
    }
}