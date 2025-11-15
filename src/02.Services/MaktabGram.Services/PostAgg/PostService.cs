using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Infrastructure.FileService.Contracts;

namespace MaktabGram.Domain.Services.PostAgg
{
    public class PostService(IPostRepository postRepository,
        IUserRepository userRepository,
        IFileService fileService) : IPostService
    {

        public Result<bool> Create(CreatePostInputDto model)
        {
            try
            {
                model.ImgUrl = fileService.Upload(model.Img, "Posts");
                model.TaggedUsers = SetUserTags(model.Tags);
                var postId = postRepository.Create(model);
                return Result<bool>.Success("پست با موفقیت ذخیره شد.");
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("ایجاد پست با خطا روبرو شد.");
            }
        }

        public List<GetPostForFeedsDto> GetFeedPosts(int userId)
        {
            return postRepository.GetFeedPosts(userId);
        }


        public List<int> SetUserTags(string postTags)
        {
            var tags = postTags.Split('#').ToList();
            var userNames = tags.Select(x => x.Trim()).ToList();
            return userRepository.GetUserIdsBy(userNames);
        }
    }
}
