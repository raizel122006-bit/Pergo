
using Pergo.Api.DTOs;
using Pergo.Api.Models;

namespace Pergo.Api.Services
{
    public interface IGoalService
    {
        Task<GoalResponseDto> CreateAsync(CreateGoalDto dto, string userId);
        Task<List<GoalResponseDto>> GetAllAsync(string userId);
        Task<GoalResponseDto?> GetByIdAsync(int id, string userId);

        Task<GoalResponseDto> UpdateAsync(UpdateGoalDto dto, string userId);
        
        Task<bool> DeleteAsync(int id, string userId);
    }
}