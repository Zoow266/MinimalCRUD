using Microsoft.EntityFrameworkCore;
using MinimalCrud.Models;

namespace MinimalCrud.DATA
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users{get;set;} = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    }
}