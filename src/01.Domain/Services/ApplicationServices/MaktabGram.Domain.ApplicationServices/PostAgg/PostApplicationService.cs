using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.FileAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Services.FileAgg.Service;
using MaktabGram.Domain.Services.PostAgg;


namespace MaktabGram.Domain.ApplicationServices.PostAgg
{
    public class PostApplicationService : IPostApplicationService
    {
        private readonly IPostService _postService;
        private readonly IFileService fileService;

        public PostApplicationService()
        {
            fileService = new FileService();
            _postService = new PostService();
        }
        public Result<bool> Create(CreatePostInputDto model)
        {
            try
            {
                model.ImgUrl = fileService.Upload(model.Img, "Posts");
                model.TaggedUsers = _postService.SetUserTags(model.Tags);
                var postId = _postService.Create(model);
                return Result<bool>.Success("پست با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("ایجاد پست با خطا روبرو شد.");
            }
        }
        public List<GetPostForFeedsDto> GetFeedPosts(int userId)
        {
           return _postService.GetFeedPosts(userId);
        }
    }
}
