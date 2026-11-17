using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace Pergo.Api.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    public DateTime DateJoined { get; set; } = DateTime.UtcNow;

    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
}