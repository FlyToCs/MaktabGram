namespace MaktabGram.Domain.UserAgg.Dtos
{
    public class RegisterUserInputDto
    {
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string? Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}