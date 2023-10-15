namespace Domain.Models
{
    public class WorkFlow
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<WorkFlowStage> Stages { get; set; }
    }
}
