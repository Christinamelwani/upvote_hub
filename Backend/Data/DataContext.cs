using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteNook> VoteNooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (
                var relationship in modelBuilder.Model
                    .GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys())
            )
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
