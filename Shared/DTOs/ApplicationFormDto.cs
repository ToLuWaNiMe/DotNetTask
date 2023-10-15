using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Shared.DTOs
{
    public class ApplicationFormDto
    {
        public IFormFile Image { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Profile Profile { get; set; }
        public AdditionalQuestions AdditionalQuestions { get; set; }
    }
}
