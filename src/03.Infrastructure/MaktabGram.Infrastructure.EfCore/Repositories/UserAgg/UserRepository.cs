using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain.UserAgg.Entities;
using MaktabGram.Domain.UserAgg.ValueObjects;
using MaktabGram.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MaktabGram.Infrastructure.EfCore.Repositories.UserAgg
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository()
        {
            dbContext = new AppDbContext();
        }

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
            var imgAddress =  dbContext.Users
                .Where(u => u.Id == userId)
                .Select(u => u.Profile.ProfileImageUrl)
                .FirstOrDefault();

            if(imgAddress is null)
                throw new NullReferenceException("Profile image URL not found.");

            return imgAddress;
        }
    }
}