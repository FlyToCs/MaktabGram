using MaktabGram.Domain.Core.PostAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabGram.Infrastructure.EfCore.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts").HasKey(p => p.Id);

            builder.Property(p => p.Caption)
                .HasMaxLength(4000)
                .IsRequired(true);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(1000)
                .IsRequired(true);

            builder.Property(p => p.OpenComment)
                .HasDefaultValue(true);

            builder.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.PostLikes)
                .WithOne(pl => pl.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.SavedByUsers)
                .WithOne(pl => pl.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.TaggedUsers)
                .WithOne(pl => pl.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
