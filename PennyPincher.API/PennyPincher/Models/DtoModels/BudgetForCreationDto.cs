using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace PennyPincher.Models.DtoModels
{
    public class BudgetForCreationDto
    {
        [MaxLength(200)]
        public string GroupName { get; set; } = string.Empty;

        [AllowNull]
        public string Type { get; set; }
    }
}
