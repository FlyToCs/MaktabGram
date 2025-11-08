using Microsoft.AspNetCore.Http;

namespace MaktabGram.Domain.Core.PostAgg.Dtos
{
    public class CreatePostInputDto
    {
        public IFormFile Img { get; set; }
        public string? ImgUrl { get; set; }
        public string Caption { get; set; }
        public string Tags { get; set; }
        public List<int>? TaggedUsers { get; set; }
        public bool ShowComment { get; set; }
        public int UserId { get; set; }
    }
}
