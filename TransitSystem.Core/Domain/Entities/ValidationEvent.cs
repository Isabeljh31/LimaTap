using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Entities
{
    public class ValidationEvent
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string TokenId { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
