using MaktabGram.Framework;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;

namespace MaktabGram.Services.UserAgg
{
    public class UserService  : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }
        public Result<bool> Login(string mobile, string password)
        {
            var login = userRepository.Login(mobile, password.ToMd5Hex());  
            
            if(login is not null)
            {
               var isActive =  userRepository.IsActive(mobile);

                return isActive
                    ? Result<bool>.Success("لاگین با موفقیت انجام شد.")
                    : Result<bool>.Failure("کاربر با این شماره فعال نمی‌باشد.");
            }
            else
            {
                 return Result<bool>.Failure("نام کاربری یا کلمه عبور اشتباه می باشد.");
            }
        }

        public Result<bool> Register(RegisterUserInputDto model)
        {

            var mobileExist = userRepository.MobileExists(model.Mobile);

            if (mobileExist)
            {
                return Result<bool>.Failure("کاربر با این شماره موجود می باشد.");
            }

            model.Password = model.Password.ToMd5Hex();

            userRepository.Register(model);

            return Result<bool>.Success("ثبت نام با موفقیت انجام شد.");
        }
    }
}
