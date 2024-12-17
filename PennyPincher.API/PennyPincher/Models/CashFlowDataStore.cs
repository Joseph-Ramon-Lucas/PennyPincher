namespace PennyPincher.Models
{
    public class CashFlowDataStore
    {
        public List<CashFlowDto> CashFlows { get; set; }
        // Singleton pattern implemented below, to ensure that we are using the same single CashFlowDataStore
        public static CashFlowDataStore CurrentCashFlow { get; } = new CashFlowDataStore();


        public CashFlowDataStore()
        {

            //dummy data
            CashFlows = new List<CashFlowDto>()
            {

                new CashFlowDto()
                {
                    Id = 1,
                    Name = "Income",
                    Description = "Salaried from Work",
                    Ammount = (70000/12),
                    Flow = FlowTypes.income

                }, 
                new CashFlowDto()
                {
                    Id = 2,
                    Name = "Apartment Rent",
                    Ammount = 1200,
                    Flow = FlowTypes.expense,
                },
                new CashFlowDto()
                {
                    Id = 3,
                    Name = "Groceries",
                    Ammount = 5000,
                    Flow = FlowTypes.expense,
                },
                new CashFlowDto()
                {
                    Id = 4,
                    Name = "Subscriptions",
                    Ammount= 100,
                    Description = "Netflix, Spotify, UberOne",
                    Flow = FlowTypes.expense,
                },
                new CashFlowDto()
                {
                    Id = 5,
                    Name = "Stocks",
                    Ammount = 40,
                    Description = "Dividends return",
                    Flow = FlowTypes.income,
                }


            };
                



        }
    }
}
