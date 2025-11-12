using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Dtos;

namespace MaktabGram.Domain.Core.PostAgg.Contracts
{
    public interface IPostService
    {
        Result<bool> Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts(int userId);
        public List<int> SetUserTags(string postTags);
    }
}
