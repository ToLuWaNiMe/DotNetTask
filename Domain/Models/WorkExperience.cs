namespace Domain.Models
{
    public class WorkExperience
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Company { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
