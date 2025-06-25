namespace PennyPincher.Models.DtoModels
{
    public class AnalysisStatusDto
    {
        public double GrossIncome { get; set; }
        public double NetIncome { get; set; }
        public double Liabilities { get; set; }
        public double NetIncomeRatio { get; set; }
        public string MostCostlyName { get; set; } = string.Empty;
        public double MostCostlyAmount { get; set; }
        public double PercentOfEarningsGoingToMostCostlyAmount { get; set; }
    }

    public class AnalysisStatusMostCostlyDto
    {
        public string Budget_Name { get; set; } = string.Empty;

        public double Amount { get; set; }
    }
}
