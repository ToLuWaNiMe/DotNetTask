using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class ApplicationForm
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public IFormFile Image { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public Profile Profile { get; set; }
        public AdditionalQuestions AdditionalQuestions { get; set; }
    }
}
