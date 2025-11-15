using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Domain.Services.FollowerAgg;

namespace MaktabGram.Domain.ApplicationServices.FollowAgg
{
    public class FollowerApplicationService (IFollowerService followerService) : IFollowerApplicationService
    {
        public void Follow(int userId, int FollowedId)
        {
            followerService.Follow(userId, FollowedId);
        }

        public void UnFollow(int userId, int FollowedId)
        {
            followerService.UnFollow(userId, FollowedId);
        }
    }
}
