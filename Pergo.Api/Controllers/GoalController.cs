
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pergo.Api.Services;
using Pergo.Api.DTOs;
using System.Security.Claims;

namespace Pergo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    // POST: api/goals
    [HttpPost]
    public async Task<IActionResult> Create(CreateGoalDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _goalService.CreateAsync(dto, userId);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // GET: api/goals
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var goals = await _goalService.GetAllAsync(userId);

        return Ok(goals);
    }

    // GET: api/goals/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var goal = await _goalService.GetByIdAsync(id, userId);

        if (goal == null)
        {
            return NotFound();
        }

        return Ok(goal);
    }

    // PUT: api/goals/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateGoalDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var result = await _goalService.UpdateAsync(id, dto, userId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // DELETE: api/goals/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }

        var deleted = await _goalService.DeleteAsync(id, userId);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
