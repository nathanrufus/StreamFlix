using Microsoft.EntityFrameworkCore;
using StreamFlix.Models;

namespace StreamFlix.Data
{
    public class StreamFlixDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public StreamFlixDbContext(DbContextOptions<StreamFlixDbContext> options) : base(options) { }
    }
}
