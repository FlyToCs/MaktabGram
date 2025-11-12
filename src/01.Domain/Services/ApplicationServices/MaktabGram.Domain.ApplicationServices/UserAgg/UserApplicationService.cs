using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.FileAgg;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Services.FileAgg.Service;
using MaktabGram.Domain.Services.UserAgg;
using MaktabGram.Framework;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;


namespace MaktabGram.Domain.ApplicationServices.UserAgg
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IUserService userService;
        private readonly IFileService fileService;

        public UserApplicationService()
        {
            userService = new UserService();
            fileService = new FileService();
        }
        public void Active(int userId)
        {
            userService.Active(userId);
        }

        public void DeActive(int userId)
        {
            userService.DeActive(userId);
        }

        public GetUserProfileDto GetProfile(int userId)
        {
            return userService.GetProfile(userId);
        }

        public UpdateGetUserDto GetUpdateUserDetails(int userId)
        {
            return userService.GetUpdateUserDetails(userId);
        }

        public List<GetUserSummaryDto> GetUsersSummary()
        {
            return userService.GetUsersSummary();
        }

        public Result<UserLoginOutputDto> Login(string mobile, string password)
        {
            var login = userService.Login(mobile, password.ToMd5Hex());

            if (login is not null)
            {
                var isActive = userService.IsActive(mobile);

                return isActive
                    ? Result<UserLoginOutputDto>.Success("لاگین با موفقیت انجام شد.", login)
                    : Result<UserLoginOutputDto>.Failure("کاربر با این شماره فعال نمی‌باشد.");
            }
            else
            {
                return Result<UserLoginOutputDto>.Failure("نام کاربری یا کلمه عبور اشتباه می باشد.");
            }
        }

        public Result<bool> Register(RegisterUserInputDto model)
        {
            return userService.Register(model);
        }

        public Result<bool> Update(int userId, UpdateGetUserDto model)
        {
            return userService.Update(userId,model );
        }

        public List<SearchResultDto> Search(string username)
        {
            return userService.Search(username);
        }
    }
}
