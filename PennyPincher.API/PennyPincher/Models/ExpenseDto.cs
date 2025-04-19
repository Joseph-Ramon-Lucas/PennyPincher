using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PennyPincher.Models
{
    public enum CategoryTypes
    {
        Undefined = 0,
        None = 1,
        Living = 2,
        Utilities = 3, 
        Entertainment = 4,
        Shopping = 5,
        Takeout = 6,
        PrimaryIncome = 7,
        SecondaryIncome = 8
    }
    
    public class ExpenseDto
    {
        [Required]  
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        
        [AllowNull]
        public CategoryTypes Category { get; set; }
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
    }   
}