using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pergo.Api.Data;
using Pergo.Api.DTOs;
using Pergo.Api.Models;

namespace Pergo.Api.Services;

public class GoalService : IGoalService
{
    private readonly AppDbContext _context;

    public GoalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GoalResponseDto> CreateAsync(CreateGoalDto dto, string userId)
    {
        var goal = new Goal
        {
            Title = dto.Title,
            Description = dto.Description,
            TargetDate = dto.TargetDate,
            UserId = userId
        };

        await _context.Goals.AddAsync(goal);
        await _context.SaveChangesAsync();


        return new GoalResponseDto
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            CreatedAt = goal.CreatedAt,
            TargetDate = goal.TargetDate,
            IsCompleted = goal.IsCompleted
        };
    }

    public async Task<List<GoalResponseDto>> GetAllAsync(string userId)
    {
        return await _context.Goals
            .Where(g => g.UserId == userId)
            .Select(g => new GoalResponseDto
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                CreatedAt = g.CreatedAt,
                TargetDate = g.TargetDate,
                IsCompleted = g.IsCompleted
            })
            .ToListAsync();
    }

    public async Task<GoalResponseDto?> GetByIdAsync(int id, string userId)
    {
    return await _context.Goals
        .Where(g => g.Id == id && g.UserId == userId)
        .Select(g => new GoalResponseDto
        {
            Id = g.Id,
            Title = g.Title,
            Description = g.Description,
            CreatedAt = g.CreatedAt,
            TargetDate = g.TargetDate,
            IsCompleted = g.IsCompleted
        })
        .FirstOrDefaultAsync();
    }

    public async Task<GoalResponseDto?> UpdateAsync(int id, UpdateGoalDto dto, string userId)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

        if (goal == null)
        {
            return null;
        }

        goal.Title = dto.Title;
        goal.Description = dto.Description;
        goal.TargetDate = dto.TargetDate;
        goal.IsCompleted = dto.IsCompleted;

        await _context.SaveChangesAsync();

        return new GoalResponseDto
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            CreatedAt = goal.CreatedAt,
            TargetDate = goal.TargetDate,
            IsCompleted = goal.IsCompleted
        };
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var goal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);

        if(goal == null)
        {
            return false ;
        }
        
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        return true;
    }
}