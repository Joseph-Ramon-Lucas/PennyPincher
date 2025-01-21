namespace PennyPincher.Models
{
    public class CashFlowDataStore
    {
        public List<CashFlowDto> CashFlowsList { get; set; }
        // Singleton pattern implemented below, to ensure that we are using the same single CashFlowDataStore
        public static CashFlowDataStore ProjectedCashFlow { get; } = new CashFlowDataStore();
        public static CashFlowDataStore CurrentItemLogCashFlow { get; } = new CashFlowDataStore();

        public CashFlowDataStore()
        {

            //dummy data
            CashFlowsList = new List<CashFlowDto>()
            {

                new CashFlowDto()
                {
                    Id = 1,
                    Name = "Income",
                    Description = "Salaried from Work",
                    Amount = (70000/12),
                    Flow = FlowTypes.income

                },
                new CashFlowDto()
                {
                    Id = 2,
                    Name = "Apartment Rent",
                    Amount = 1200,
                    Flow = FlowTypes.expense,
                    Category = CategoryTypes.Living
                },
                new CashFlowDto()
                {
                    Id = 3,
                    Name = "Groceries",
                    Amount = 5000,
                    Flow = FlowTypes.expense,
                    Category = CategoryTypes.Takeout
                },
                new CashFlowDto()
                {
                    Id = 4,
                    Name = "Subscriptions",
                    Amount= 100,
                    Description = "Netflix, Spotify, UberOne",
                    Flow = FlowTypes.expense,
                },
                new CashFlowDto()
                {
                    Id = 5,
                    Name = "Stocks",
                    Amount = 40,
                    Description = "Dividends return",
                    Flow = FlowTypes.income,
                }


            };
                



        }
    }
}
