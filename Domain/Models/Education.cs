namespace Domain.Models
{
    public class Education
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string School { get; set; }
        public string Degree { get; set; }
        public string Course { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
