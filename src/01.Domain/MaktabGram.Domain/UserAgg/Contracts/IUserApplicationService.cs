using MaktabGram.Domain.Core._common.Entities;
using MaktabGram.Domain.Core.UserAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Domain.Core.UserAgg.Contracts
{
    public interface IUserApplicationService
    {
        public Result<UserLoginOutputDto> Login(string mobile, string password);
        public Result<bool> Register(RegisterUserInputDto model);
        List<GetUserSummaryDto> GetUsersSummary();
        public UpdateGetUserDto GetUpdateUserDetails(int userId);
        public void Active(int userId);
        public void DeActive(int userId);
        public Result<bool> Update(int userId, UpdateGetUserDto model);
        public GetUserProfileDto GetProfile(int userId);
        public List<SearchResultDto> Search(string username, int userId);
        public string GetImageProfileUrl(int userId);
    }
}
