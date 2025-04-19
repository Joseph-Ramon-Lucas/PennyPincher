using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models
{
    public class ExpenseForUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required for item creation")]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public CategoryTypes Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
    }
};