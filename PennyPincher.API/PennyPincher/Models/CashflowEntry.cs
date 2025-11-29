using PennyPincher.Models.DtoModels;
using static PennyPincher.Models.TypeCollections;

namespace PennyPincher.Models
{
    public class CashflowEntry
    {
        public int cashflow_entry_id { get; set; }

        public string cashflow_entry_name { get; set; } = String.Empty;

        public string description { get; set; } = String.Empty;

        public double amount { get; set; }

        public DateTime entry_date { get; set; }

        public CashflowType flow { get; set; }

        public CategoryTypes category_type { get; set; }

        public int user_id { get; set; }
    }
}
