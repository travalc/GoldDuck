using Microsoft.EntityFrameworkCore;

namespace GoldDuck.Models
{
    public class GoldDuckContext : DbContext
    {
        public GoldDuckContext(DbContextOptions<GoldDuckContext> options) : base(options) { } 

        public DbSet<User> users {get; set;}
        public DbSet<Idea> ideas {get; set;}
        public DbSet<Like> likes {get; set;}
    }
}