using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models
{
    public class ItemForUpdateDto
    {
        [Required(ErrorMessage = "Name is required for item creation")]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Price is required for item creation")]
        public double Price { get; set; }
        
        public CategoryTypes Category { get; set; }
    }
};