using MaktabGram.Domain.PostAgg.Contracts;
using MaktabGram.Domain.PostAgg.Dtos;
using MaktabGram.Domain.PostAgg.Entities;
using MaktabGram.Framework;
using MaktabGram.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;


namespace MaktabGram.Infrastructure.EfCore.Repositories.PostAgg
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext appDbContext;

        public PostRepository()
        {
            appDbContext = new AppDbContext();
        }

        public List<GetPostForFeedsDto>GetFeedPosts()
        {
            var posts = appDbContext.Posts.Include(p=>p.Comments).ThenInclude(c=>c.User)
                .Select(p => new GetPostForFeedsDto
            {
                Caption = p.Caption,
                ImgPostUrl = p.ImageUrl,
                LikeCount = p.PostLikes.Count,
                Username = p.User.Username,
                CreateAt = p.CreatedAt.ToPersianString("dddd, dd MMMM,yyyy"),
                ProfileImgUrl = p.User.Profile.ProfileImageUrl,
                Comments = p.Comments
            }).ToList();

            return posts;
        }

        public int Create(CreatePostInputDto model)
        {
            var post = new Post
            {
                Caption = model.Caption,
                ImageUrl = model.ImgUrl!,
                OpenComment = model.ShowComment,
                UserId = model.UserId,
                CreatedAt = DateTime.Now,
                TaggedUsers = model.TaggedUsers.Select(x=>new PostTag()
                {
                    TaggedUserId = x,
                }).ToList(),
            };

            appDbContext.Posts.Add(post);
            appDbContext.SaveChanges();

            return post.Id;
        }
    }
}
