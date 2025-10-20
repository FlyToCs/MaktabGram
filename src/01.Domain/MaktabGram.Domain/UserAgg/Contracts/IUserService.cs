using MaktabGram.Domain._common.Entities;
using MaktabGram.Domain.UserAgg.Dtos;

namespace MaktabGram.Domain.UserAgg.Contracts
{
    public interface IUserService
    {
        public Result<bool> Login(string mobile, string password);
        public Result<bool> Register(RegisterUserInputDto model);
    }
}
