using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.Interfaces;
using TransitSystem.Shared.Models;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransitIssueController : ControllerBase
    {
        private readonly ITransitIssueService _issueService;

        public TransitIssueController(ITransitIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpPost("report")]
        public IActionResult ReportIssue([FromBody] TransitIssueDto issue)
        {
            var result = _issueService.RegisterIssue(issue);
            if (!result) return BadRequest(new { Message = "Datos del reporte de transporte inválidos." });

            return Ok(new { Success = true, Message = "Alerta de sistema unificado recibida con éxito.", Status = issue.Status });
        }
    }
}
