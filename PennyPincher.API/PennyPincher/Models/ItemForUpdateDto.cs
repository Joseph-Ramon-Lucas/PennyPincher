using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models
{
    public class ItemForUpdateDto
    {
        [Required(ErrorMessage = "Name is required for item creation")]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
        
        [AllowNull]  
        public CategoryTypes Category { get; set; }
    }
};