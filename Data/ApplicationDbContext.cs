using Microsoft.EntityFrameworkCore;
using _1stModule_PIPremises.Models;

namespace _1stModule_PIPremises.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<SwipeLog> SwipeLogs { get; set; }
        public DbSet<User> Users { get; set; }
        
        // ✅ Register the Permit model
        public DbSet<Permit> Permits { get; set; }
    }
}
