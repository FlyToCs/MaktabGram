using MaktabGram.Domain.PostAgg.Contracts;
using MaktabGram.Domain.PostAgg.Dtos;
using MaktabGram.Domain.PostAgg.Entities;
using MaktabGram.Infrastructure.EfCore.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Infrastructure.EfCore.Repositories.PostAgg
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext appDbContext;

        public PostRepository()
        {
            appDbContext = new AppDbContext();
        }
        public int Create(CreatePostInputDto model)
        {
            var post = new Post
            {
                Caption = model.Caption,
                ImageUrl = model.ImgUrl!,
                OpenComment = model.ShowComment,
                UserId = model.UserId,
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
