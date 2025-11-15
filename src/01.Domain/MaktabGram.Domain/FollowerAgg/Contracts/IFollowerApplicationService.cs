using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Domain.Core.FollowerAgg.Contracts
{
    public interface IFollowerApplicationService
    {
        public void Follow(int userId, int FollowedId);
        public void UnFollow(int userId, int FollowedId);
    }
}
