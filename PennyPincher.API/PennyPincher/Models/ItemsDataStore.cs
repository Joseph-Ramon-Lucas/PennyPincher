namespace PennyPincher.Models;

public class ItemsDataStore
{
    public List<ItemDto> Items { get; set; }
    
    // Singleton pattern implemented below, to ensure that we are using the same single ItemsDataStore
    public static ItemsDataStore Current { get; } = new ItemsDataStore();

    public ItemsDataStore()
    {
        // Init dummy data
        Items = new List<ItemDto>()
        {
            new ItemDto()
            {
                Id = 1,
                Name = "Starbucks Coffee",
                Category = CategoryTypes.Takeout,
                Price = 4.50
            },
            new ItemDto()
            {
                Id = 2,
                Name = "Netflix Subscription",
                Category = CategoryTypes.Entertainment,
                Price = 15.20
            },
            new ItemDto()
            {
                Id = 3,
                Name = "January 2025 Rent",
                Category = CategoryTypes.Living,
                Price = 1120
            }
        };
    }
}