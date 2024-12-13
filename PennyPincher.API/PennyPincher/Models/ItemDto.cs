namespace PennyPincher.Models
{
    public enum categoryTypes
    {
        Utilities, 
        Entertainment,
        Shopping
    }
    
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public categoryTypes Category { get; set; }
        
        public double Price { get; set; }
    }   
}