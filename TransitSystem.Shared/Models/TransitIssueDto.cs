using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Shared.Models
{
    public class TransitIssueDto
    {
        public string IssueId { get; set; } = "INC-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        public string CardNumber { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string SystemType { get; set; } = "Metropolitano"; // o Línea 1
        public string Description { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Abierto";
    }
}
