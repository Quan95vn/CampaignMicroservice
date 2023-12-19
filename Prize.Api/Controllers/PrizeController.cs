using Microsoft.AspNetCore.Mvc;

namespace Prize.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PrizeController : ControllerBase
{
    private readonly ILogger<PrizeController> _logger;

    public PrizeController(ILogger<PrizeController> logger)
    {
        _logger = logger;
    }
}