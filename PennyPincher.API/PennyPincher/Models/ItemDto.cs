namespace PennyPincher.Models
{
    public enum CategoryTypes
    {
        Living,
        Utilities, 
        Entertainment,
        Shopping,
        Takeout
    }
    
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public CategoryTypes Category { get; set; }
        
        public double Price { get; set; }
    }   
}