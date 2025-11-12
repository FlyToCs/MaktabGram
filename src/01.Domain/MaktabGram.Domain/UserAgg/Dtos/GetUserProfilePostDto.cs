namespace MaktabGram.Domain.Core.UserAgg.Dtos
{
    public class GetUserProfilePostDto
    {
        public int PostId { get; set; }
        public string ImgPostUrl { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }

    }
}
