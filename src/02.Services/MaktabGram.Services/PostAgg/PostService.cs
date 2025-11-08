using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.FileAgg;
using MaktabGram.Domain.PostAgg.Contracts;
using MaktabGram.Domain.PostAgg.Dtos;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Repositories.PostAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Services.FileAgg.Service;
using System.Net;

namespace MaktabGram.Services.PostAgg
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

        private List<int> SetUserTags(string postTags)
        {
            var tags = postTags.Split('#').ToList();
            var userNames = tags.Select(x => x.Trim()).ToList();
            return userRepository.GetUserIdsBy(userNames);
        }
    }
}
