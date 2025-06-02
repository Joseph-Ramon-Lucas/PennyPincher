using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public enum AnalysisTypes
{
    Pie = 0,
    Bar = 1, 
    Line = 2
}

namespace PennyPincher.Models.DtoModels
{
    public class AnalysisDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required for analysis creation")]
        [MaxLength(200)]
        public string Name { get; set; }

        [AllowNull]
        public AnalysisTypes Type { get; set; }        
    }
}
