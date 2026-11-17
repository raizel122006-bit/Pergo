using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pergo.Api.Models;

namespace Pergo.Api.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{   
    public DbSet<Goal> Goals { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    
    }
}