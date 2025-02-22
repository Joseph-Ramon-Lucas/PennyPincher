namespace PennyPincher.Models;

public class ExpenseDataStore
{
    public List<ExpenseDto> Expenses { get; set; }
    
    // Singleton pattern implemented below, to ensure that we are using the same single ItemsDataStore
    public static ExpenseDataStore Current { get; } = new ExpenseDataStore();

    public ExpenseDataStore()
    {
        // Init dummy data
        Expenses = new List<ExpenseDto>()
        {
            new ExpenseDto()
            {
                Id = 1,
                Name = "Starbucks Coffee",
                Category = CategoryTypes.Takeout,
                Price = 4.50
            },
            new ExpenseDto()
            {
                Id = 2,
                Name = "Netflix Subscription",
                Category = CategoryTypes.Entertainment,
                Price = 15.20
            },
            new ExpenseDto()
            {
                Id = 3,
                Name = "January 2025 Rent",
                Category = CategoryTypes.Living,
                Price = 1120
            }
        };
    }
}