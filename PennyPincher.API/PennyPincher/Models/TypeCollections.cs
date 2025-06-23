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
            Takeout = 5
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
    }
}
