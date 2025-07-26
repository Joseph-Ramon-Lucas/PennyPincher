using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public class UserDto
    {


        [Required(ErrorMessage = "A Name is required for this User")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
    }
}
