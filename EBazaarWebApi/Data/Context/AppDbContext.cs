using EBazaarWebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EBazaarWebApi.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define your DbSets (tables) here
        public DbSet<Product> Products { get; set; }
    }
}
