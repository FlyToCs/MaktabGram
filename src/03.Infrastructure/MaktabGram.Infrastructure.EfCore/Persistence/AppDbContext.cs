using MaktabGram.Domain.CommentAgg.Entities;
using MaktabGram.Domain.PostAgg.Entities;
using MaktabGram.Domain.UserAgg.Entities;
using MaktabGram.Infrastructure.EfCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace MaktabGram.Infrastructure.EfCore.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Follower> Follows { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=MaktabGramDb;user id=sa;password=25915491;trust server certificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }

}
