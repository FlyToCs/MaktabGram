using MaktabGram.Framework;
using MaktabGram.Domain.Services.UserAgg;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Infrastructure.FileService.Services;
using MaktabGram.Infrastructure.FileService.Contracts;


namespace MaktabGram.Domain.ApplicationServices.UserAgg
{
    public class UserApplicationService(IUserService userService,
        IFileService fileService) : IUserApplicationService
    {
        public void Active(int userId)
        {
            userService.Active(userId);
        }

        public void DeActive(int userId)
        {
            userService.DeActive(userId);
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
            return userService.Update(userId, model);
        }

        public List<SearchResultDto> Search(string username, int userId)
        {
            return userService.Search(username, userId);
        }

        public GetUserProfileDto GetProfile(int searchedUserId, int curentUserId)
        {
            return userService.GetProfile(searchedUserId, curentUserId);
        }

        public string GetImageProfileUrl(int userId)
        {
            return userService.GetImageProfileUrl(userId);
        }
    }
}
