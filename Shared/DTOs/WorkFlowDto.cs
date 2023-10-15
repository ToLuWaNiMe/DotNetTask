using Domain.Models;

namespace Shared.DTOs
{
    public class WorkFlowDto
    {
        public string Name { get; set; }
        public List<WorkFlowStage> Stages { get; set; }
    }
}
