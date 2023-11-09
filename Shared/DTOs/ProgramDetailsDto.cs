using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class ProgramDetailsDto
    {
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        public string Skills { get; set; }
        public string Benefits { get; set; }
        public string Criteria { get; set; }
        public ProgramInformationDto ProgramInformation { get; set; }

    }
}
