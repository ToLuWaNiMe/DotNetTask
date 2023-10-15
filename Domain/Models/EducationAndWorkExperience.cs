namespace Domain.Models
{
    public class EducationAndWorkExperienceForm
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Education Education { get; set; }
        public WorkExperience WorkExperience { get; set; }
    }
}
