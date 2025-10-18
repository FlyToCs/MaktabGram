using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.CommentAgg.Entities;
using MaktabGram.Domain.UserAgg.Entities;

namespace MaktabGram.Domain.PostAgg.Entities;
public class Post : BaseEntity
{
    public string Caption { get; set; }
    public bool OpenComment { get; set; }
    public string ImageUrl { get; set; }


    #region NavigationProperties
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Comment> Comments { get; set; }
    public List<PostLike> PostLikes { get; set; }
    public List<PostSave> SavedByUsers { get; set; }
    public List<PostTag> TaggedUsers { get; set; }
    #endregion

}
