using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Shared.DTOs
{
    public class PreviewDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
        public string Benefits { get; set; }
        public string Criteria { get; set; }
        public IFormFile Image { get; set; }
        public ProgramInformation ProgramInformation { get; set; }
    }
}
