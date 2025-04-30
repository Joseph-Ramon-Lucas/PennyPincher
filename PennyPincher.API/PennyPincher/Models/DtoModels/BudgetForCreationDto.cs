using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public class BudgetForCreationDto
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public string Type { get; set; }
    }
}
