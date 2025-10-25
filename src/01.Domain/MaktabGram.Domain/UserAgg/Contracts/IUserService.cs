using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Dtos;

namespace MaktabGram.Domain.UserAgg.Contracts
{
    public interface IUserService
    {
        public Result<UserLoginOutputDto> Login(string mobile, string password);
        public Result<bool> Register(RegisterUserInputDto model);
        List<GetUserSummaryDto> GetUsersSummary();
        public void Active(int userId);
        public void DeActive(int userId);
    }
}
