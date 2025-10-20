using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Dtos;

namespace MaktabGram.Domain.UserAgg.Contracts
{
    public interface IUserRepository
    {
        public Result<bool> Login(string mobile, string password);
        public Result<bool> Register(RegisterUserInputDto model);
        public Result<bool> IsActive(string mobile);
        public Result<bool> MobileExists(string mobile);
    }
}
