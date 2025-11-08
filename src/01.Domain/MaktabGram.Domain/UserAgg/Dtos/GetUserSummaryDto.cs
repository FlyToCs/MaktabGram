namespace MaktabGram.Domain.Core.UserAgg.Dtos
{
    public class GetUserSummaryDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; }
        public string? CreateAtFa { get; set; }
        public string? ImageProfileUrl { get; set; }

    }
}
