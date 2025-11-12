using MaktabGram.Domain.Core.PostAgg.Dtos;

namespace MaktabGram.Domain.Core.PostAgg.Contracts
{
    public interface IPostRepository
    {
        public int Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts(int userId);
    }
}
