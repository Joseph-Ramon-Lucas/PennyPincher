using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models
{
    public class User
    {
        public int Id { get; set; }

        public int TokenId { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
    }
}
