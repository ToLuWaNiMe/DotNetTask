namespace Domain.Models
{
    public class AdditionalQuestions
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SelfDescription { get; set; }
        public int YearOfGraduation { get; set; }
        public string Question { get; set; }
        public string Choice { get; set; }
        public bool Rejected { get; set; }
    }
}
