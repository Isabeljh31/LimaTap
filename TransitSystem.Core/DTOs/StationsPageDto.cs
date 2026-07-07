using System.Collections.Generic;

namespace TransitSystem.Shared.Models
{
    public class StationsPageDto
    {
        public List<StationDto> Stations { get; set; } = new();
        public string RecommendedRoute { get; set; } = string.Empty;
        public string EstimatedTime { get; set; } = string.Empty;
        public string NextService { get; set; } = string.Empty;
        public Dictionary<string, string> ServiceStatus { get; set; } = new();
        public string Tip { get; set; } = string.Empty;
    }

    public class StationDto
    {
        public string Name { get; set; } = string.Empty;
        public string Line { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string StatusClass { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
