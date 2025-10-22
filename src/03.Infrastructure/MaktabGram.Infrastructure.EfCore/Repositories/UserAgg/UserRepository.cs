using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain.UserAgg.Entities;
using MaktabGram.Domain.UserAgg.ValueObjects;
using MaktabGram.Infrastructure.EfCore.Persistence;

namespace MaktabGram.Infrastructure.EfCore.Repositories.UserAgg
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository()
        {
            dbContext = new AppDbContext();
        }

        public bool IsActive(string mobile)
        {
            return dbContext.Users.Any(u => u.Mobile == Mobile.Create(mobile) && u.IsActive);
        }

        public UserLoginOutputDto? Login(string mobile, string password)
        {
            var user = dbContext.Users
            .Where(u => u.Mobile.Value == mobile && u.PasswordHash == password)
            .AsEnumerable()
            .Select(u=> new UserLoginOutputDto
            {
                Id = u.Id,
                Username=u.Username,
                Email = u.Profile.Email,
                Mobile = mobile,
                FirstName = u.Profile.FirstName,
                LastName = u.Profile.LastName
            }).AsEnumerable().FirstOrDefault();

            return user;
        }

        public bool MobileExists(string mobile)
        {
            return dbContext.Users.Any(u => u.Mobile == Mobile.Create(mobile));
        }

        public bool Register(RegisterUserInputDto model)
        {
            var entity = new User
            {
                Mobile = Mobile.Create(model.Mobile),
                Username = model.Username,
                PasswordHash = model.Password,
                Profile = new UserProfile
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                }
            };

            dbContext.Users.Add(entity);
            return dbContext.SaveChanges() > 1;
        }
    }
}
