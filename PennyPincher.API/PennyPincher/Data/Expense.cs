namespace PennyPincher.Data;

public enum CategoryTypes
{
    Undefined = 0,
    None = 1,
    Living = 1,
    Utilities = 2, 
    Entertainment = 3,
    Shopping = 4,
    Takeout = 5
}

public class Expense
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public CategoryTypes Category { get; set; }
    
    public double Price { get; set; } 
}