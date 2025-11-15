using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Repositories.FollowerAgg;

namespace MaktabGram.Domain.Services.FollowerAgg
{
    public class FollowerService (IFollowerRepository followerRepository) : IFollowerService
    {

        public void Follow(int userId, int FollowedId)
        {
            followerRepository.Follow(userId, FollowedId);
        }

        public void UnFollow(int userId, int FollowedId)
        {
            followerRepository.UnFollow(userId, FollowedId);
        }
    }
}
