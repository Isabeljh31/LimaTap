using Microsoft.AspNetCore.Mvc;
using TransitSystem.Shared.Models;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStations()
        {
            var payload = new StationsPageDto
            {
                RecommendedRoute = "Metropolitano · Naranjal → Central",
                EstimatedTime = "18 min",
                NextService = "2 min",
                Tip = "Si vas al centro, usa la estación Central para un mejor tiempo de espera y menos trasbordos.",
                ServiceStatus = new Dictionary<string, string>
                {
                    ["Metropolitano"] = "Operativo",
                    ["Línea 1"] = "Operativa",
                    ["Línea 2"] = "En construcción"
                },
                Stations = new List<StationDto>
                {
                    new() { Name = "Estación Central", Line = "Metropolitano", Distance = "2 min", Status = "Operativo", StatusClass = "good", Color = "var(--met)" },
                    new() { Name = "Atocongo", Line = "Línea 1", Distance = "6 min", Status = "Operativo", StatusClass = "good", Color = "var(--l1)" },
                    new() { Name = "Naranjal", Line = "Metropolitano", Distance = "4 min", Status = "Normal", StatusClass = "good", Color = "var(--met)" },
                    new() { Name = "San Borja Norte", Line = "Línea 1", Distance = "8 min", Status = "Demora 5 min", StatusClass = "warning", Color = "var(--l1)" }
                }
            };

            return Ok(payload);
        }
    }
}
