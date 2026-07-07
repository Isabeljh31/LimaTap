using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.Services;
using TransitSystem.Shared.Models;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketingController : ControllerBase
    {
        private readonly TicketingProcessor _ticketingProcessor;

        public TicketingController(TicketingProcessor ticketingProcessor)
        {
            _ticketingProcessor = ticketingProcessor;
        }

        [HttpPost("validate-tap")]
        public async Task<IActionResult> ValidateTap([FromBody] TapRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "El cuerpo de la solicitud no puede ser nulo."
                });
            }

            if (string.IsNullOrWhiteSpace(request.TokenId) || string.IsNullOrWhiteSpace(request.SystemType) || string.IsNullOrWhiteSpace(request.StationId))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Todos los campos son obligatorios."
                });
            }

            bool validated = await _ticketingProcessor.ProcessTapInAsync(request.TokenId, request.SystemType, request.StationId);

            if (!validated)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Validación fallida: token inválido, saldo insuficiente o estación no aceptada."
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Viaje validado correctamente."
            });
        }
    }
}
