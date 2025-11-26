using MaktabGram.Domain.Core.PostAgg.Entities;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Core.UserAgg.Entities;
using MaktabGram.Domain.Core.UserAgg.ValueObjects;
using MaktabGram.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabGram.Infrastructure.EfCore.Repositories.UserAgg
{
    public class UserRepository (AppDbContext dbContext) : IUserRepository
    {

        public void Active(int userId)
        {
            dbContext.Users
               .ExecuteUpdate(setters => setters
                   .SetProperty(u => u.IsActive, true));
        }

        public void DeActive(int userId)
        {
            dbContext.Users
               .ExecuteUpdate(setters => setters
                   .SetProperty(u => u.IsActive, false));
        }

        public List<GetUserSummaryDto> GetUsersSummary()
        {
            var user = dbContext.Users
            .Select(u => new GetUserSummaryDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Profile.Email,
                Mobile = u.Mobile.Value,
                FirstName = u.Profile.FirstName,
                LastName = u.Profile.LastName,
                IsAdmin = u.IsAdmin,
                Status = u.IsActive,
                CreateAt = u.CreatedAt,
                ImageProfileUrl = u.Profile.ProfileImageUrl,
            }).ToList();

            return user;
        }

        public bool IsActive(string mobile)
        {
            var mobileValue = Mobile.Create(mobile);

            return dbContext.Users.Any(u => u.Mobile.Value == mobileValue.Value && u.IsActive);
        }

        public UserLoginOutputDto? Login(string mobile, string password)
        {
            var user = dbContext.Users
            .Where(u => u.Mobile.Value == mobile && u.PasswordHash == password)
            .Select(u => new UserLoginOutputDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Profile.Email,
                Mobile = mobile,
                FirstName = u.Profile.FirstName,
                LastName = u.Profile.LastName,
                IsAdmin = u.IsAdmin,
            }).AsEnumerable().FirstOrDefault();

            return user;
        }


        public bool MobileExists(string mobile)
        {
            return dbContext.Users.Any(u => u.Mobile.Value == mobile);
        }

        public bool Register(RegisterUserInputDto model)
        {
            var entity = new User
            {
                Mobile = Mobile.Create(model.Mobile),
                Username = model.Username,
                PasswordHash = model.Password,
                CreatedAt = DateTime.Now,
                Profile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfileImageUrl = model.ProfileImageUrl,
                }
            };

            dbContext.Users.Add(entity);
            return dbContext.SaveChanges() > 1;
        }

        public UpdateGetUserDto GetUpdateUserDetails(int userId)
        {
            var user = dbContext.Users
            .Where(x => x.Id == userId)
            .AsNoTracking()
            .Select(u => new UpdateGetUserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Profile.Email,
                Mobile = u.Mobile.Value,
                FirstName = u.Profile.FirstName,
                LastName = u.Profile.LastName,
                IsAdmin = u.IsAdmin,
                ImageProfileUrl = u.Profile.ProfileImageUrl,
            }).FirstOrDefault();

            return user;
        }



        public bool Update(int userId, UpdateGetUserDto model)
        {
            try
            {
                var user = dbContext.Users
                .Include(u => u.Profile)
                .FirstOrDefault(u => u.Id == userId);

                if (user is not null)
                {
                    user.Username = model.Username;
                    user.Mobile.Value = model.Mobile;
                    user.IsAdmin = model.IsAdmin;
                    user.Profile.Email = model.Email;
                    user.Profile.FirstName = model.FirstName;
                    user.Profile.LastName = model.LastName;

                    user.PasswordHash = (!string.IsNullOrEmpty(model.Password)) ?
                        model.Password : user.PasswordHash;

                    if (!string.IsNullOrEmpty(model.ImageProfileUrl))
                        user.Profile.ProfileImageUrl = model.ImageProfileUrl;




                    dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public string GetImageProfileUrl(int userId)
        {
            var imgAddress = dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Profile.ProfileImageUrl)
                .FirstOrDefault();

            if (imgAddress is null)
                throw new NullReferenceException("Profile image URL not found.");

            return imgAddress;
        }

        public List<int> GetUserIdsBy(List<string> userNames)
        {
            return dbContext.Users
                .Where(u => userNames.Contains(u.Username))
                .Select(u => u.Id).ToList();
        }

        public GetUserProfileDto GetProfile(int searchedUserId)
        {
            var profile = dbContext.Users
                .Where(u => u.Id == searchedUserId)
                .Select(u => new GetUserProfileDto
                {
                    Id = u.Id,
                    UserName = u.Username,
                    Bio = u.Profile.Bio,
                    ImgProfileUrl = u.Profile.ProfileImageUrl,
                    FollowerCount = u.Followers.Count,
                    FollowingCount = u.Followings.Count,
                });

           
            return profile.FirstOrDefault();
        }

        public GetUserProfileDto GetProfileWithPosts(int searchedUserId, int curentUserId)
        {
            var profile = dbContext.Users
                .Where(u => u.Id == searchedUserId)
                .Select(u => new GetUserProfileDto
                {
                    Id = u.Id,
                    UserName = u.Username,
                    Bio = u.Profile.Bio,
                    ImgProfileUrl = u.Profile.ProfileImageUrl,
                    FollowerCount = u.Followers.Count,
                    FollowingCount = u.Followings.Count,
                    Posts = u.Posts.Select(p => new GetUserProfilePostDto
                    {
                        PostId = p.Id,
                        CommentCount = p.Comments.Count,
                        LikeCount = p.PostLikes.Count,
                        ImgPostUrl = p.ImageUrl
                    }).ToList(),
                    SavedPosts = u.SavedPosts.Select(sp => new GetUserProfilePostDto
                    {
                        PostId = sp.PostId,
                        CommentCount = sp.Post.Comments.Count,
                        LikeCount = sp.Post.PostLikes.Count,
                        ImgPostUrl = sp.Post.ImageUrl
                    }).ToList(),
                    TagPosts = u.TaggedPosts.Select(tp => new GetUserProfilePostDto
                    {
                        PostId = tp.PostId,
                        CommentCount = tp.Post.Comments.Count,
                        LikeCount = tp.Post.PostLikes.Count,
                        ImgPostUrl = tp.Post.ImageUrl
                    }).ToList(),
                });

           
            return profile.FirstOrDefault();
        }

        public List<SearchResultDto> Search(string username , int userId)
        {
            var query = dbContext.Users.OrderByDescending(x => x.CreatedAt)
                .Select(u => new SearchResultDto
                {
                    UserId = u.Id,
                    UserName = u.Username,
                    ImgProfileUrl = u.Profile.ProfileImageUrl,
                    IsFollowed = u.Followers.Any(f => f.FollowerId == userId)
                }).Take(10);

            if(!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.UserName.Contains(username));
            }

            return query.ToList();
        }

        public bool IsFolllow(int searchedUserId, int curentUserId)
        {
            return dbContext.Followers.Any(f => f.FollowerId == curentUserId && f.FollowedId == searchedUserId);
        }
    }
}