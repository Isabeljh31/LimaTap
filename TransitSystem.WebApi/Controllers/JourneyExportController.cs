using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.DTOs;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyExportController : ControllerBase
    {
        private readonly IJourneyExportService _exportService;

        public JourneyExportController(IJourneyExportService exportService)
        {
            _exportService = exportService;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> Export(
            string accountId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] bool includeMetropolitano = true,
            [FromQuery] bool includeLinea1 = true)
        {
            var options = new JourneyExportOptions
            {
                AccountId = accountId,
                From = from,
                To = to,
                IncludeMetropolitano = includeMetropolitano,
                IncludeLinea1 = includeLinea1
            };

            var fileBytes = await _exportService.ExportAsync(options);
            var fileName = $"historial-viajes-{accountId}.csv";

            return File(fileBytes, "text/csv; charset=utf-8", fileName);
        }
    }
}
