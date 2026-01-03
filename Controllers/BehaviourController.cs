using CatBreedingProgram.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatBreedingProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BehaviourController : ControllerBase
{
    private readonly ILogger<BehaviourController> _logger;
    private readonly IBehaviourService _behaviourService;

    public BehaviourController(ILogger<BehaviourController> logger, IBehaviourService behaviourService)
    {
        _logger = logger;
        _behaviourService = behaviourService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] BehaviourLogRequest request)
    {
        _logger.LogInformation($"[BehaviourController] invoked - Post behaviour data: {request}");

        _behaviourService.LogBehaviour(request);

        return Ok("Behaviour data received. Thank you! :D");
    }
}