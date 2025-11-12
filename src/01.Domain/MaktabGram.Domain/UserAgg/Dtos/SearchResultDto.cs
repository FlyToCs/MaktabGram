
namespace MaktabGram.Domain.Core.UserAgg.Dtos
{
    public class SearchResultDto
    {
        public int UserId { get; set; }
        public string? ImgProfileUrl { get; set; }
        public string? UserName { get; set; }
        public bool IsFollowed { get; set; }
    }
}
