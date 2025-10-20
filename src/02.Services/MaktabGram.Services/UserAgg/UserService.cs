using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Framework;

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
            
            if(login.IsSuccess)
            {
               var isActive =  userRepository.IsActive(mobile);

                if(isActive.IsSuccess)
                {
                    return new Result<bool> {IsSuccess = true , Message = login.Message ,Data = isActive.IsSuccess };
                }
                else
                {
                    return new Result<bool> { IsSuccess = false, Message = isActive.Message, Data = isActive.IsSuccess };
                }
            }
            else
            {
                return new Result<bool> { IsSuccess = false, Message = login.Message, Data = login.IsSuccess };
            }
        }

        public Result<bool> Register(RegisterUserInputDto model)
        {

            var mobileExist = userRepository.MobileExists(model.Mobile);

            if (mobileExist.IsSuccess)
            {
                return new Result<bool> { IsSuccess = false, Message = "کاربر با این شماره موجود می باشد.", Data = false };
            }

            model.Password = model.Password.ToMd5Hex();

            return userRepository.Register(model);
        }
    }
}
