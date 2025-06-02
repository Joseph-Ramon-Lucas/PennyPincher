using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public enum CashflowTypes
    {
        Income = 0,
        Expense = 1
    }

    public abstract class CashflowEntryDto
    {
        public int Amount { get; set; }

        [Required(ErrorMessage = "A FlowType (Income / Expense) is required for this CashFlow")]
        [EnumDataType(typeof(CashflowTypes))]
        public CashflowTypes Flow { get; set; }
    }
}
