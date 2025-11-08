using MaktabGram.Domain.Core.UserAgg.Enum;
using System.ComponentModel.DataAnnotations;

namespace MaktabGram.Domain.Core.UserAgg.Entities;

public class UserProfile
{
    public int UserId { get; set; }
    public User User { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsPrivate { get; private set; }
    public DateOnly BirthDate { get; set; }
    public GenderEnum Gender { get; set; }

    #region Behaviars
    public void SetPrivate() => IsPrivate = true;
    public void SetPublic() => IsPrivate = false;
    #endregion

}