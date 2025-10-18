using System.ComponentModel.DataAnnotations;

namespace MaktabGram.Domain.UserAgg.Enum
{
    public enum GenderEnum
    {
        [Display(Name = "آقا")]
        Male = 1,
        [Display(Name = "خانم")]
        Female = 2
    }
}