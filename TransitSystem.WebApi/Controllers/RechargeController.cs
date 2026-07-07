using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransitSystem.Core.Services;
using TransitSystem.Core.DTOs;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RechargeController : ControllerBase
    {
        private readonly RechargeService _rechargeService;

        public RechargeController(RechargeService rechargeService)
        {
            _rechargeService = rechargeService;
        }

        [HttpPost]
        public async Task<IActionResult> Recharge([FromBody] RechargeRequest request)
        {
            var transaction = await _rechargeService.ProcessWebRechargeAsync(
                request.AccountId,
                request.Amount,
                request.PaymentToken,
                "CreditCard"
            );

            return Ok(new RechargeResponse
            {
                TransactionId = transaction.TransactionId,
                Status = transaction.Status.ToString(),
                Message = "Recarga procesada exitosamente."
            });
        }
    }
}