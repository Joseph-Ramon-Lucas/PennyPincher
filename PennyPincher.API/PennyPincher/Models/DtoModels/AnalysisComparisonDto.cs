using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Models.DtoModels
{
    public class AnalysisComparisonDto
    {
        public IEnumerable<CashflowEntryDto> CurrentTopExpenses { get; set; } = Enumerable.Empty<CashflowEntryDto>();
        public IEnumerable<CashflowEntryDto> ProjectedTopExpenses { get; set; } = Enumerable.Empty<CashflowEntryDto>();
        public IEnumerable<CashflowEntryDto> CurrentCategorySum { get; set; } = Enumerable.Empty<CashflowEntryDto>();
        public IEnumerable<CashflowEntryDto> ProjectedCategorySum { get; set; } = Enumerable.Empty<CashflowEntryDto>();
        public double CurrentMostCostlyCategoryAmount { get; set; }
        public double ProjectedMostCostlyCategoryAmount { get; set; }
        public CategoryTypes CurrentMostCostlyCategory { get; set; }
        public CategoryTypes ProjectedMostCostlyCategory { get; set; }
        public CategoryTypes ProjectedMostCostlyCurrentCategoryDisplay { get; set; }
        public double CostlyCategoryRatio { get; set; }
    }
}
