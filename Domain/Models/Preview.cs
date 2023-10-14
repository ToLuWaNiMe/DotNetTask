using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Preview
    {
        public string ProgramId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Skills { get; set; }
        public string Benefits { get; set; }
        public string Criteria { get; set; }
        public IFormFile Image { get; set; }
        public ProgramInformation ProgramInformation { get; set; }

    }
}
