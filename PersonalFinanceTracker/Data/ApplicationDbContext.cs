using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Entities;

namespace PersonalFinanceTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
    }
}
