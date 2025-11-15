namespace MaktabGram.Domain.Core.FollowerAgg.Contracts
{
    public interface IFollowerService
    {
        public void Follow(int userId, int FollowedId);
        public void UnFollow(int userId, int FollowedId);
    }
}
