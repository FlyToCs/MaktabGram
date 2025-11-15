using MaktabGram.Domain.Core.FollowerAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MaktabGram.Infrastructure.EfCore.Configurations
{
    public class FollowerEntityConfigurations : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.ToTable("Followers");
            builder.HasKey(cl => new { cl.FollowedId, cl.FollowerId });
        }
    }
}
