using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi;

public class ApplicationContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
}