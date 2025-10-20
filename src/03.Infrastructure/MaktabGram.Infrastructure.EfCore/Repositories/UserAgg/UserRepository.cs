using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain.UserAgg.Entities;
using MaktabGram.Domain.UserAgg.ValueObjects;
using MaktabGram.Infrastructure.EfCore.Persistence;

namespace MaktabGram.Infrastructure.EfCore.Repositories.UserAgg
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDbContext dbContext;
        public UserRepository()
        {
            dbContext = new AppDbContext();
        }

        public Result<bool> IsActive(string mobile)
        {
            var isActive = dbContext.Users.Any(u => u.Mobile == Mobile.Create(mobile) && u.IsActive);


            if (isActive)
                return new Result<bool> { IsSuccess = true, Message = "کاربر فعال می باشد.", Data = isActive };

            return new Result<bool> { IsSuccess = false, Message = "کاربر غیر فعال می باشد.", Data = isActive };
        }

        public Result<bool> Login(string mobile, string password)
        {
            var login =
                 dbContext.Users.Any(u => u.Mobile.Value == mobile && u.PasswordHash == password);

            if (login)
                return new Result<bool> { IsSuccess = true, Message = "عملیات لاگین با موفقیت انجام شد.", Data = login };

            return new Result<bool> { IsSuccess = false, Message = "شماره موبایل یا کلمه عبور اشتباه است.", Data = login };
        }

        public Result<bool> MobileExists(string mobile)
        {
            var exist = dbContext.Users.Any(u => u.Mobile == Mobile.Create(mobile));


            if (exist)
                return new Result<bool> { IsSuccess = true, Message = "شماره موبایل موجود می باشد.", Data = exist };

            return new Result<bool> { IsSuccess = false, Message = "شماره موبایل موجود نمی باشد.", Data = exist };
        }

        public Result<bool> Register(RegisterUserInputDto model)
        {
            try
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
                dbContext.SaveChanges();
                return new Result<bool> { IsSuccess = true, Message = "ایجاد کاربر با موفقیت انجام شد." };

            }
            catch
            {
                return new Result<bool> { IsSuccess = false, Message = "خطا در ایجاد کاربر." };
            }
        }
    }
}
