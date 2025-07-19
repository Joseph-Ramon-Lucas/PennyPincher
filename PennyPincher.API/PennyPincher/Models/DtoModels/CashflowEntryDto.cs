using System.ComponentModel.DataAnnotations;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Models.DtoModels
{


    public class CashflowEntryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        [Required(ErrorMessage = "An amount is required for this CashFlow")]
        public double Amount { get; set; }

        public DateTime EntryDate { get; set; }

        [Required(ErrorMessage = "A FlowType (Income / Expense) is required for this CashFlow")]
        [EnumDataType(typeof(CashflowType))]
        public CashflowType Flow { get; set; }
        
        [EnumDataType(typeof(CategoryTypes))]
        public CategoryTypes CategoryType { get; set; }
    }
}
