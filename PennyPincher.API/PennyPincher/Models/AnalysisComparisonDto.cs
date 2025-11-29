namespace PennyPincher.Models
{
    public class AnalysisComparisonDto
    {
        public IEnumerable<CashFlowDto> CurrentTopExpenses { get; set; } = Enumerable.Empty<CashFlowDto>();
        public IEnumerable<CashFlowDto> ProjectedTopExpenses { get; set; } = Enumerable.Empty<CashFlowDto>();
        public IEnumerable<CashFlowDto> CurrentCategorySum { get; set; } = Enumerable.Empty<CashFlowDto>();
        public IEnumerable<CashFlowDto> ProjectedCategorySum { get; set; } = Enumerable.Empty<CashFlowDto>();
        public double CurrentMostCostlyCategoryAmount { get; set; }
        public double ProjectedMostCostlyCategoryAmount { get; set; }
        public CategoryTypes CurrentMostCostlyCategory { get; set; }
        public CategoryTypes ProjectedMostCostlyCategory { get; set; }
        public CategoryTypes ProjectedMostCostlyCurrentCategoryDisplay { get; set; }
        public double CostlyCategoryRatio { get; set; }
    }
}
