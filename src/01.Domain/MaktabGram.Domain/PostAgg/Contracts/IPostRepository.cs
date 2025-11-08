using MaktabGram.Domain.PostAgg.Dtos;

namespace MaktabGram.Domain.PostAgg.Contracts
{
    public interface IPostRepository
    {
        public int Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts();
    }
}
