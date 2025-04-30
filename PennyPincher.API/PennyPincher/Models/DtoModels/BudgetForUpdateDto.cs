using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PennyPincher.Models.DtoModels
{
    public class BudgetForUpdateDto
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
