using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Dtos;
using MaktabGram.Domain.UserAgg.Entities;

namespace MaktabGram.Domain.UserAgg.Contracts
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
    }
}
