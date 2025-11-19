using Azure;
using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Persistence;
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

        public List<GetPostForFeedsDto> GetFeedPosts(int userId, int page, int pageSize)
        {
            return postRepository.GetFeedPosts(userId,page,pageSize);
        }

        public int GetPostCount(int userId)
        {
            return postRepository.GetPostCount(userId);
        }

        public void Like(int userId, int PostId)
        {
            postRepository.Like(userId, PostId);
        }

        public bool UserLikePost(int userId, int PostId)
        {
            return postRepository.UserLikePost(userId,PostId);
        }

        public List<int> SetUserTags(string postTags)
        {
            var tags = postTags.Split('#').ToList();
            var userNames = tags.Select(x => x.Trim()).ToList();
            return userRepository.GetUserIdsBy(userNames);
        }

        public void DisLike(int userId, int PostId)
        {
             postRepository.DisLike(userId, PostId);
        }
    }
}
