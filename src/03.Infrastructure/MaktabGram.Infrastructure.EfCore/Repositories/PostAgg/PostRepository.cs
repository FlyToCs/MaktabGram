using Azure;
using MaktabGram.Domain.Core.PostAgg.Contracts;
using MaktabGram.Domain.Core.PostAgg.Dtos;
using MaktabGram.Domain.Core.PostAgg.Entities;
using MaktabGram.Framework;
using MaktabGram.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace MaktabGram.Infrastructure.EfCore.Repositories.PostAgg
{
    public class PostRepository(AppDbContext appDbContext) : IPostRepository
    {

        public GetPostDetailsDto? GetPostDetails(int postId)
        {
            var post = appDbContext.Posts.Include(p => p.Comments).ThenInclude(c => c.User)
                .Where(x => x.Id == postId)
                .Select(p => new GetPostDetailsDto
                {
                    Id = p.Id,
                    Caption = p.Caption,
                    ImgPostUrl = p.ImageUrl,
                    LikeCount = p.PostLikes.Count,
                    Username = p.User.Username,
                    CreateAt = p.CreatedAt.ToPersianString("dddd, dd MMMM,yyyy"),
                    ProfileImgUrl = p.User.Profile.ProfileImageUrl,
                    Comments = p.Comments,
                }).FirstOrDefault();

            return post;
        }

        public List<GetPostForFeedsDto> GetFeedPosts(int userId, int page, int pageSize)
        {

            var posts = appDbContext.Posts.Include(p => p.Comments).ThenInclude(c => c.User)
               .Where(x => x.User.Followers.Any(x => x.FollowerId == userId))
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * pageSize).Take(pageSize)
                .Select(p => new GetPostForFeedsDto
                {
                    Id = p.Id,
                    Caption = p.Caption,
                    ImgPostUrl = p.ImageUrl,
                    LikeCount = p.PostLikes.Count,
                    Username = p.User.Username,
                    CreateAt = p.CreatedAt.ToPersianString("dddd, dd MMMM,yyyy"),
                    ProfileImgUrl = p.User.Profile.ProfileImageUrl,
                    Comments = p.Comments,
                    UserLikeThisPost = p.PostLikes.Any(x => x.UserId == userId),
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
                TaggedUsers = model.TaggedUsers.Select(x => new PostTag()
                {
                    TaggedUserId = x,
                }).ToList(),
            };

            appDbContext.Posts.Add(post);
            appDbContext.SaveChanges();

            return post.Id;
        }

        public int GetPostCount(int userId)
        {
            var posts = appDbContext.Posts
               .Count(x => x.User.Followers.Any(x => x.FollowerId == userId));
            return posts;
        }

        public void Like(int userId, int PostId)
        {
            var postLike = new PostLike
            {
                LikedAt = DateTime.Now,
                PostId = PostId,
                UserId = userId
            };

            appDbContext.PostLikes.Add(postLike);
            appDbContext.SaveChanges();
        }

        public bool UserLikePost(int userId, int PostId)
        {
            return appDbContext.PostLikes.Any(x=>x.UserId == userId && x.PostId == PostId);
        }

        public void DisLike(int userId, int PostId)
        {
             appDbContext.PostLikes.Where(x=>x.UserId == userId && x.PostId == PostId).ExecuteDelete();
        }
    }
}
