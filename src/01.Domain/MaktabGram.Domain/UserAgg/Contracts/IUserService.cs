using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.UserAgg.Dtos;

namespace MaktabGram.Domain.Core.UserAgg.Contracts
{
    public interface IUserService
    {
        public UserLoginOutputDto? Login(string mobile, string password);
        public Result<bool> Register(RegisterUserInputDto model);
        public bool IsActive(string mobile);
        public bool MobileExists(string mobile);
        public List<GetUserSummaryDto> GetUsersSummary();
        public UpdateGetUserDto GetUpdateUserDetails(int userId);
        public void Active(int userId);
        public void DeActive(int userId);
        public Result<bool> Update(int userId, UpdateGetUserDto model);
        public string GetImageProfileUrl(int userId);
        public List<int> GetUserIdsBy(List<string> userNames);
        public GetUserProfileDto GetProfile(int userId);
        public List<SearchResultDto> Search(string username, int userId);
    }
}
