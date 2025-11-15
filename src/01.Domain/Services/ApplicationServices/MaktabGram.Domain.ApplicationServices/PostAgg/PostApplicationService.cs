using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Services.PostAgg;
using MaktabGram.Infrastructure.FileService.Contracts;
using MaktabGram.Infrastructure.FileService.Services;


namespace MaktabGram.Domain.ApplicationServices.PostAgg
{
    public class PostApplicationService (IPostService postService ,
        IFileService fileService) : IPostApplicationService
    {
        public Result<bool> Create(CreatePostInputDto model)
        {
            try
            {
                model.ImgUrl = fileService.Upload(model.Img, "Posts");
                model.TaggedUsers = postService.SetUserTags(model.Tags);
                var postId = postService.Create(model);
                return Result<bool>.Success("پست با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("ایجاد پست با خطا روبرو شد.");
            }
        }
        public List<GetPostForFeedsDto> GetFeedPosts(int userId)
        {
           return postService.GetFeedPosts(userId);
        }
    }
}
