using Microsoft.AspNetCore.Mvc;
using CatBreedingProgram.Models;

namespace CatBreedingProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatsController : ControllerBase
{

    [HttpPost]
    public IActionResult Post([FromBody] Cat cat)
    {
        return Ok("You are creating a " + (cat.IsMale ? "Male" : "Female") + " cat named " + cat.Name + " who is " + cat.Age + " years old and has " + cat.FurColourHex + " fur.");
    }
}