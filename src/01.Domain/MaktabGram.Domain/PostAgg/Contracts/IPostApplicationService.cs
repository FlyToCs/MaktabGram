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
        public List<GetPostForFeedsDto> GetFeedPosts(int userId, int page, int pageSize);
        public int GetPostCount(int userId);
        public void Like(int userId, int PostId);
        public bool UserLikePost(int userId, int PostId);
        public void DisLike(int userId, int PostId);
        public GetPostDetailsDto? GetPostDetails(int postId);
    }
}
