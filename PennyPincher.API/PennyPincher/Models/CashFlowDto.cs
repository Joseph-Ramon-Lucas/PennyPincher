using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PennyPincher.Models
{

    public enum FlowTypes
    {
        income,
        expense
    }
    public class CashFlowDto
    {
        [Required(ErrorMessage ="An integer ID is required for this CashFlow")]
        [Range(0,int.MaxValue)]
        public int Id { get; set; }


        [Required(ErrorMessage = "A name is required for this CashFlow")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "CF Name should be between 1 and 500 characters")]
        public string Name { get; set; } = string.Empty;


        [AllowNull]
        [StringLength(500, MinimumLength = 0, ErrorMessage = "CF Description should be between 0 and 500 characters")]
        public string? Description { get; set; }


        // assume money is measured by a monthly basis
        [Required(ErrorMessage = "An amount of money is required for this CashFlow")]
        public double Amount { get; set; }


        [Required(ErrorMessage = "A FlowType (Income / Expense) is required for this CashFlow")]
        [EnumDataType(typeof(FlowTypes))]
        public FlowTypes Flow { get; set; }
    }
}
