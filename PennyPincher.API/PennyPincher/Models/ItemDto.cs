using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PennyPincher.Models
{
    public enum CategoryTypes
    {
        None = 0,
        Living = 1,
        Utilities = 2, 
        Entertainment = 3,
        Shopping = 4,
        Takeout = 5
    }
    
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        [AllowNull]
        public CategoryTypes Category { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
    }   
}