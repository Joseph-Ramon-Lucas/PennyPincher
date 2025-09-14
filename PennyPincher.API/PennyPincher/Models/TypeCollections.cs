namespace PennyPincher.Models
{
    public class TypeCollections
    {
        public enum CategoryTypes
        {
            None = 0,
            Living = 1,
            Utilities = 2,
            Entertainment = 3,
            Shopping = 4,
            Takeout = 5,
            Housing = 6,
            Transportation = 7,
            Food = 8,
            Health = 9,
            Income = 10,
            Job = 11,
            Freelance = 12,
            Gift = 13,
            Investment = 14
        }

        public enum AnalysisTypes
        {
            Pie = 0,
            Bar = 1,
            Line = 2
        }

        public enum BudgetTypes
        {
            Undefined = 0,
            FiftyTwentyThirty = 1,
            PayYourselfFirst = 2,
            ZeroBased = 3
        }

        public enum CashflowType
        {
            Income = 0,
            Expense = 1
        }
    }
}
