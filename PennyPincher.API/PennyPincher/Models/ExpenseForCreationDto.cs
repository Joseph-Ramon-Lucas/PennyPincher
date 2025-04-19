using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models
{
    public class ExpenseForCreationDto
    {
        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required for item creation")]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public string Category { get; set; }

        [Required(ErrorMessage = "Price is required for item creation")]
        public double Price { get; set; }
    }    
}
