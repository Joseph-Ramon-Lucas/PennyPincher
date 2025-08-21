using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public class UserDto
    {
        public int Id { get; set; }

        public int TokenId { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
    }

    public class UserMadeObjectIds
    {
        public int cashflow_entry_id { get; set; }
        public int cashflow_group_id { get; set; }
    }
}
