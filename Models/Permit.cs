using System.ComponentModel.DataAnnotations;

namespace _1stModule_PIPremises.Models
{
    public class Permit
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Permit Number cannot exceed 10 characters.")]
        public string PermitNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Type of Permit")]
        [RegularExpression("Hot|Cold|Height", ErrorMessage = "Permit Type must be Hot, Cold, or Height.")]
        public string PermitType { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Issue Date and Time")]
        public DateTime IssueDateTime { get; set; }

        [Required]
        [Display(Name = "Functional Location")]
        public string FunctionalLocation { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Station Name")]
        public string StationName { get; set; } = string.Empty;
    }
}
