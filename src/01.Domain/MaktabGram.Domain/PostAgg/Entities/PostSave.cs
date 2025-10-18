using MaktabGram.Domain.UserAgg.Entities;

namespace MaktabGram.Domain.PostAgg.Entities;
public class PostSave
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public DateTime SavedAt { get; set; }
}