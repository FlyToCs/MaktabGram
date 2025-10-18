using MaktabGram.Domain.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Domain.PostAgg.Entities
{
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int TaggedUserId { get; set; }
        public User TaggedUser { get; set; }
    }
}
