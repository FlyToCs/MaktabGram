using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.PostAgg.Dtos;

namespace MaktabGram.Domain.PostAgg.Contracts
{
    public interface IPostService
    {
        Result<bool> Create(CreatePostInputDto model);
        public List<GetPostForFeedsDto> GetFeedPosts();
    }
}
