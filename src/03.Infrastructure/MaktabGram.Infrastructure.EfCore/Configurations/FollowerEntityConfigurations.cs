using MaktabGram.Domain.Core.UserAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MaktabGram.Infrastructure.EfCore.Configurations
{
    public class FollowerEntityConfigurations : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.HasKey(cl => new { cl.FollowedId, cl.FollowerId });
        }
    }
}
