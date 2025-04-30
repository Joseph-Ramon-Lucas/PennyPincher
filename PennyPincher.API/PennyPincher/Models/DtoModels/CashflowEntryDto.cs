using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public enum FlowTypes
    {
        Income = 0,
        Expense = 1
    }

    public abstract class CashflowEntryDto
    {
        public int Amount { get; set; }

        [Required(ErrorMessage = "A FlowType (Income / Expense) is required for this CashFlow")]
        [EnumDataType(typeof(FlowTypes))]
        public FlowTypes Flow { get; set; }
    }
}
