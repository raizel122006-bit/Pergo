using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Pergo.Api.Models;

public class Goal
{

    public int Id { get; set; } 

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;

    public DateTime? TargetDate { get; set; }

    public bool IsCompleted { get; set; } = false;

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;


}