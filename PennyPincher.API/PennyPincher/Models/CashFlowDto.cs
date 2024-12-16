namespace PennyPincher.Models
{

    public enum FlowTypes
    {
        income,
        expense
    }
    public class CashFlowDto
    {
        private int Id { get; set; }
        private string Name { get; set; } = string.Empty;
        private string? Description { get; set; }
        private FlowTypes Flow { get; set; }

    }
}
