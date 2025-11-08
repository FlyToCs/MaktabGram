namespace MaktabGram.Domain.Core.UserAgg.Entities;

public class Follower
{
    #region properties

    public int FollowerId { get; set; }
    public User FollowerUser { get; set; }

    public int FollowedId { get; set; } 
    public User FollowedUser { get; set; }

    public DateTime FollowAt { get; set; }
    public bool IsBlock { get; private set; }

    #endregion

    #region Behaviars
    public void Block() => IsBlock = true;
    public void UnBlock() => IsBlock = false;
    #endregion

}
