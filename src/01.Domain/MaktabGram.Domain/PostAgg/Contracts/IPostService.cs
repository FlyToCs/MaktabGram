using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Dtos;

namespace MaktabGram.Domain.Core.PostAgg.Contracts
{
    public interface IPostService
    {
        Result<bool> Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts(int userId, int page, int pageSize);
        public List<int> SetUserTags(string postTags);
        public int GetPostCount(int userId);
        public void Like(int userId, int PostId);
        public bool UserLikePost(int userId, int PostId);
        public void DisLike(int userId, int PostId);
        public GetPostDetailsDto? GetPostDetails(int postId);
    }
}
