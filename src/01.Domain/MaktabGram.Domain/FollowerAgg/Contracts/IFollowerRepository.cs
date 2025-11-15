namespace MaktabGram.Domain.Core.FollowerAgg.Contracts
{
    public interface IFollowerRepository
    {
        public void Follow(int userId, int FollowedId);
        public void UnFollow(int userId, int FollowedId);
    }
}
