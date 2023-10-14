using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class WorkFlowDto
    {
        public string Name { get; set; }
        public List<WorkFlowStage> Stages { get; set; }
    }
}
