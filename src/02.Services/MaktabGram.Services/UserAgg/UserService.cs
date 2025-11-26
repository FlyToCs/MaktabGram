using MaktabGram.Framework;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.UserAgg.Contracts;
using MaktabGram.Infrastructure.FileService.Contracts;

namespace MaktabGram.Domain.Services.UserAgg
{
    public class UserService (IUserRepository userRepository , IFileService fileService) : IUserService
    {
        public List<GetUserSummaryDto> GetUsersSummary()
        {
            var users = userRepository.GetUsersSummary();

            users.ForEach(user =>
                user.CreateAtFa = user.CreateAt.ToPersianString("yyyy/MM/dd"));

            return users;
        }
        public void Active(int userId) => userRepository.Active(userId);
        public void DeActive(int userId) => userRepository.DeActive(userId);
        public UserLoginOutputDto? Login(string mobile, string password) => userRepository.Login(mobile, password);
        public UpdateGetUserDto GetUpdateUserDetails(int userId) => userRepository.GetUpdateUserDetails(userId);
        public Result<bool> Update(int userId, UpdateGetUserDto model)
        {
            if (model.ImgProfile is not null)
            {
                var existingImageUrl = userRepository.GetImageProfileUrl(userId);
                fileService.Delete(existingImageUrl);
                model.ImageProfileUrl = fileService.Upload(model.ImgProfile!, "Profiles");
            }

            if (model.Password is not null)
            {
                model.Password = model.Password.ToMd5Hex();
            }

            var result = userRepository.Update(userId, model);

            if (result)
            {
                return Result<bool>.Success("اطلاعات کاربر با موفقیت به‌روزرسانی شد.");
            }
            else
            {
                return Result<bool>.Failure("به‌روزرسانی اطلاعات کاربر با خطا مواجه شد.");
            }
        }
        public bool IsActive(string mobile) => userRepository.IsActive(mobile);
        public bool MobileExists(string mobile) => userRepository.MobileExists(mobile);
        public string GetImageProfileUrl(int userId) => userRepository.GetImageProfileUrl(userId);
        public List<int> GetUserIdsBy(List<string> userNames) => userRepository.GetUserIdsBy(userNames);
        public Result<bool> Register(RegisterUserInputDto model) 
        {
            var mobileExist = MobileExists(model.Mobile);

            if (mobileExist)
            {
                return Result<bool>.Failure("کاربر با این شماره موجود می باشد.");
            }

            if (model.ProfileImg is not null)
            {
                model.ProfileImageUrl = fileService.Upload(model.ProfileImg, "Profiles");
            }

            model.Password = model.Password.ToMd5Hex();

            userRepository.Register(model);

            return Result<bool>.Success("ثبت نام با موفقیت انجام شد.");
        }

        public GetUserProfileDto GetProfile(int searchedUserId, int curentUserId)
        {
            if(userRepository.IsFolllow(searchedUserId, curentUserId))
            {
                var user = userRepository.GetProfileWithPosts(searchedUserId, curentUserId);
                user.IsFollower = true;
                return user;
            }

            return userRepository.GetProfile(searchedUserId);
        }

        public List<SearchResultDto> Search(string username,int userId)
        {
            return userRepository.Search(username,userId);
        }

    }
}
