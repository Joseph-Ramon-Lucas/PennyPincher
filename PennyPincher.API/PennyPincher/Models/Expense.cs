namespace PennyPincher.Models;

public class Expense
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Category { get; set; }
    
    public double Price { get; set; } 
}