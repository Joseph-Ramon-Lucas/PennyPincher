using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public enum BudgetTypes
{
    Undefined = 0,
    FiftyTwentyThirty = 1,
    PayYourselfFirst = 2,
    ZeroBased = 3
}

namespace PennyPincher.Models.DtoModels
{
    public class BudgetDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required for budget creation")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public BudgetTypes Type { get; set; } 
    }
}
