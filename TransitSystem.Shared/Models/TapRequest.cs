using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Shared.Models
{
    public class TapRequest
    {
        public string TokenId { get; set; } = string.Empty;
        public string SystemType { get; set; } = string.Empty;
        public string StationId { get; set; } = string.Empty;
    }
}
