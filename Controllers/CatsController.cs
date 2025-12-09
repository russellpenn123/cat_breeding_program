using Microsoft.AspNetCore.Mvc;
using CatBreedingProgram.Models;

namespace CatBreedingProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{
    private readonly ILogger<CatsController> _logger;

    public CatsController(ILogger<CatsController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Cat cat)
    {
        _logger.LogInformation("CatsController invoked - Registering cat: {CatName}, Age: {Age}, Gender: {Gender}, Color: {Color}",
            cat.Name, cat.Age, cat.IsMale ? "Male" : "Female", cat.FurColourHex);

        return Ok("You are creating a " + (cat.IsMale ? "Male" : "Female") + " cat named " + cat.Name + " who is " + cat.Age + " years old and has " + cat.FurColourHex + " fur. Cute!");
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("CatsContoller invoked - Get all cats");
        return Ok("Returning all registered cats: { mocky moggy }");   
    }
}