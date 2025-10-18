using MaktabGram.Domain.PostAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaktabGram.Infrastructure.EfCore.Configurations
{
    public class PostTagConfigurations : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(cl => new { cl.PostId, cl.TaggedUserId });
        }
    }
}
