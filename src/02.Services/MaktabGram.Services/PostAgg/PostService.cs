using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.FileAgg;
using MaktabGram.Domain.PostAgg.Contracts;
using MaktabGram.Domain.PostAgg.Dtos;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Repositories.PostAgg;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Services.FileAgg.Service;

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
            

            model.ImgUrl = fileService.Upload(model.Img, "Posts");

            var tags = model.Tags.Split('#').ToList();
            var userNames = tags.Select(x => x.Trim()).ToList();
            model.TaggedUsers = userRepository.GetUserIdsBy(userNames);

            var postId = postRepository.Create(model);

            throw new NotImplementedException();
        }
    }
}
