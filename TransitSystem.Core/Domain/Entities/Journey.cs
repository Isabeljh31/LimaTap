using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Entities
{
    public class Journey
    {
        public string JourneyId { get; set; } = Guid.NewGuid().ToString();
        public string AccountId { get; set; } = string.Empty;
        public string OriginStationId { get; set; } = string.Empty;
        public string DestinationStationId { get; set; } = string.Empty;
        public decimal FareApplied { get; set; }
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }
    }
}
