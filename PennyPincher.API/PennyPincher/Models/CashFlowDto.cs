namespace PennyPincher.Models
{

    public enum FlowTypes
    {
        income,
        expense
    }
    public class CashFlowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        // assume money is measured by a monthly basis
        public double Ammount { get; set; }
        public FlowTypes Flow { get; set; }

    }
}
