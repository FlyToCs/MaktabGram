using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.FileAgg;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Services.FileAgg.Service;
using MaktabGram.Infrastructure.EfCore.Repositories.PostAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using System.Net;

namespace MaktabGram.Domain.Services.PostAgg
{
    public class PostService : IPostService
    {
        private readonly IFileService fileService;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        public PostService()
        {
            fileService = new FileService();
            postRepository = new PostRepository();
            userRepository = new UserRepository();
        }
        public Result<bool> Create(CreatePostInputDto model)
        {
            try
            {
                throw new Exception();
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

        public List<GetPostForFeedsDto> GetFeedPosts()
        {
            return postRepository.GetFeedPosts();
        }

        public List<int> SetUserTags(string postTags)
        {
            var tags = postTags.Split('#').ToList();
            var userNames = tags.Select(x => x.Trim()).ToList();
            return userRepository.GetUserIdsBy(userNames);
        }
    }
}
