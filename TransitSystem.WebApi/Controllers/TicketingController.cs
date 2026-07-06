using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
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
        public async Task<IActionResult> ValidateTap([FromBody] JsonElement payload)
        {
            var request = new TapRequest();

            try
            {
                if (payload.ValueKind == JsonValueKind.Object)
                {
                    if (payload.TryGetProperty("tokenId", out var tokenId))
                    {
                        request.TokenId = tokenId.GetString() ?? string.Empty;
                    }

                    if (payload.TryGetProperty("systemType", out var systemType))
                    {
                        request.SystemType = systemType.GetString() ?? string.Empty;
                    }

                    if (payload.TryGetProperty("stationId", out var stationId))
                    {
                        request.StationId = stationId.GetString() ?? string.Empty;
                    }

                    if (payload.TryGetProperty("TokenId", out var tokenIdCamel))
                    {
                        request.TokenId = tokenIdCamel.GetString() ?? request.TokenId;
                    }

                    if (payload.TryGetProperty("SystemType", out var systemTypeCamel))
                    {
                        request.SystemType = systemTypeCamel.GetString() ?? request.SystemType;
                    }

                    if (payload.TryGetProperty("StationId", out var stationIdCamel))
                    {
                        request.StationId = stationIdCamel.GetString() ?? request.StationId;
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "El cuerpo de la solicitud no tiene un formato JSON válido."
                });
            }

            if (string.IsNullOrWhiteSpace(request.TokenId) ||
                string.IsNullOrWhiteSpace(request.SystemType) ||
                string.IsNullOrWhiteSpace(request.StationId))
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Completa token, sistema y estación para validar el viaje."
                });
            }

            var validated = await _ticketingProcessor.ProcessTapInAsync(
                request.TokenId,
                request.SystemType,
                request.StationId);

            return Ok(new ApiResponse
            {
                Success = validated,
                Message = validated
                    ? "Viaje validado correctamente."
                    : "No se pudo validar el viaje. Revisa el saldo o la tarjeta.",
                NewBalance = 0m
            });
        }
    }
}
