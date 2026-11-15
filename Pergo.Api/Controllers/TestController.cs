using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pergo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("protected")]
    [Authorize]
    public IActionResult Protected()
    {
        return Ok(new
        {
            message = "You are authenticated!",
            userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
            fullName = User.Identity?.Name
        });
    }
}