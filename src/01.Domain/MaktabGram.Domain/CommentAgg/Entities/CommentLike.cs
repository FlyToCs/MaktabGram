using MaktabGram.Domain.Core.UserAgg.Entities;

namespace MaktabGram.Domain.Core.CommentAgg.Entities;

public class CommentLike
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int CommentId { get; set; }
    public Comment Comment { get; set; }

    public DateTime LikedAt { get; set; }
}
