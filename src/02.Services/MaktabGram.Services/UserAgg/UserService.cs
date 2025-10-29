using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.FileAgg;
using MaktabGram.Domain.UserAgg.Contracts;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Framework;
using MaktabGram.Infrastructure.EfCore.Repositories.UserAgg;
using MaktabGram.Services.FileAgg.Service;
using Microsoft.IdentityModel.Tokens;

namespace MaktabGram.Services.UserAgg
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IFileService fileService;

        public UserService()
        {
            userRepository = new UserRepository();
            fileService = new FileService();
        }

        public void Active(int userId)
         => userRepository.Active(userId);


        public void DeActive(int userId)
         => userRepository.DeActive(userId);

        public List<GetUserSummaryDto> GetUsersSummary()
        {
            var users = userRepository.GetUsersSummary();

            users.ForEach(user =>
                user.CreateAtFa = user.CreateAt.ToPersianString("yyyy/MM/dd"));

            return users;
        }

        public Result<UserLoginOutputDto> Login(string mobile, string password)
        {
            var login = userRepository.Login(mobile, password.ToMd5Hex());

            if (login is not null)
            {
                var isActive = userRepository.IsActive(mobile);

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
            var mobileExist = userRepository.MobileExists(model.Mobile);

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

        public UpdateGetUserDto GetUpdateUserDetails(int userId)
        {
            var user = userRepository.GetUpdateUserDetails(userId);
            return user;
        }

        public Result<bool> Update(int userId, UpdateGetUserDto model)
        {
            if (model.ImgProfile is not null)
            {
                var existingImageUrl = userRepository.GetImageProfileUrl(userId);
                fileService.Delete(existingImageUrl);
                model.ImageProfileUrl = fileService.Upload(model.ImgProfile!, "Profiles");
            }

            if(model.Password is not null)
            {
                model.Password = model.Password.ToMd5Hex();
            }

            var result = userRepository.Update(userId, model);

            if(result)
            {
                return Result<bool>.Success("اطلاعات کاربر با موفقیت به‌روزرسانی شد.");
            }
            else
            {
                return Result<bool>.Failure("به‌روزرسانی اطلاعات کاربر با خطا مواجه شد.");
            }   
        }
    }
}
