using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Domain.Core.PostAgg.Contracts
{
    public interface IPostApplicationService
    {
        Result<bool> Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts();
    }
}
