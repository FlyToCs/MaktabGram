using MaktabGram.Domain.Core.UserAgg.Dtos;

namespace MaktabGram.Domain.Core.UserAgg.Contracts
{
    public interface IUserRepository
    {
        public UserLoginOutputDto? Login(string mobile, string password);
        public bool Register(RegisterUserInputDto model);
        public bool IsActive(string mobile);
        public bool MobileExists(string mobile);
        public List<GetUserSummaryDto> GetUsersSummary();
        public UpdateGetUserDto GetUpdateUserDetails(int userId);
        public void Active(int userId);
        public void DeActive(int userId);
        public bool Update(int userId, UpdateGetUserDto model);
        public string GetImageProfileUrl(int userId);
        public List<int> GetUserIdsBy(List<string> userNames);
        public GetUserProfileDto GetProfile(int searchedUserId);
        public List<SearchResultDto> Search(string username,int userId);
        public GetUserProfileDto GetProfileWithPosts(int searchedUserId, int curentUserId);
        public bool IsFolllow(int searchedUserId, int curentUserId);
    }
}
