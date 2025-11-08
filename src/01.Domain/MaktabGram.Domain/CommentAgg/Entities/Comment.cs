using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Entities;
using MaktabGram.Domain.Core.UserAgg.Entities;

namespace MaktabGram.Domain.Core.CommentAgg.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public int? ParentId { get; set; }
    public Comment Parent { get; set; }
    public List<Comment> Replies { get; set; }

    public List<CommentLike> CommentLikes { get; set; }
}