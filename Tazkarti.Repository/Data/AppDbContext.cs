using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tazkarti.Core.Models;

namespace Tazkarti.Repository.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Event> Events { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Match> Matches { get; set; }
    
    public DbSet<Ticket> Tickets { get; set; }
}