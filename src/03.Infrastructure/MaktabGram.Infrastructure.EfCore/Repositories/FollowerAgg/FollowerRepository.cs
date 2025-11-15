using MaktabGram.Domain.Core.FollowerAgg.Entities;
using MaktabGram.Domain.Core.FollowerAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Persistence;

namespace MaktabGram.Infrastructure.EfCore.Repositories.FollowerAgg
{
    public class FollowerRepository (AppDbContext dbContext) : IFollowerRepository
    {
        public void Follow(int userId, int FollowedId)
        {
            var entity = new Follower
            {
                FollowerId = userId,
                FollowedId = FollowedId,
                FollowAt = DateTime.Now,
            };

            dbContext.Followers.Add(entity);
            dbContext.SaveChanges();
        }

        public void UnFollow(int userId, int FollowedId)
        {
            var follow = dbContext.Followers
                .FirstOrDefault(f => f.FollowerId == userId && f.FollowedId == FollowedId);
            if (follow is not null)
            {
                dbContext.Followers.Remove(follow);
                dbContext.SaveChanges();
            }
        }

    }
}
