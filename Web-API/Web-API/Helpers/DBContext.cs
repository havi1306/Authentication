using Microsoft.EntityFrameworkCore;
using Web_API.Entities;

namespace Web_API.Helpers
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options)
            : base(options)
        {
        }
            
        public DbSet<Role> Roles { get; set; }
            
        public DbSet<User> Users { get; set; }
    }
}    