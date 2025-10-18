using MaktabGram.Domain.PostAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Infrastructure.EfCore.Configurations
{
    public class PostSaveConfigurations : IEntityTypeConfiguration<PostSave>
    {
        public void Configure(EntityTypeBuilder<PostSave> builder)
        {
            builder.HasKey(cl => new { cl.PostId, cl.UserId });
        }
    }
}
