using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace PennyPincher.Models.DtoModels
{
    public class AnalysisForUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required for analysis creation")]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [AllowNull]
        public AnalysisTypes Type { get; set; }
    }
}
