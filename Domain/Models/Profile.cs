namespace Domain.Models
{
    public class Profile
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
    }
}
