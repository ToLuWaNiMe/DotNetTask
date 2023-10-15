using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class ProgramInformationDto
    {
        [Required]
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime ApplicationOpen { get; set; }
        [Required]
        public DateTime ApplicationClose { get; set; }
        public string Duration { get; set; }
        [Required]
        public string Location { get; set; }
        public string Qualification { get; set; }
        public int NumberOfApplication { get; set; }
    }
}
