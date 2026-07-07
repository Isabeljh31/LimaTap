using System;

namespace TransitSystem.Core.DTOs
{
    public class JourneyExportOptions
    {
        public string AccountId { get; set; } = string.Empty;
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IncludeMetropolitano { get; set; } = true;
        public bool IncludeLinea1 { get; set; } = true;
    }
}
