using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaktabGram.Domain.UserAgg.Dtos
{
    public class UpdateGetUserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string? ImageProfileUrl { get; set; }
        public IFormFile? ImgProfile { get; set; }
    }
}
