using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.Interfaces;
using TransitSystem.WebApi.Mocks;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyRepository _journeyRepository = new MockJourneyRepository();

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetHistory(string accountId)
        {
            var journeys = await _journeyRepository.GetJourneysByAccountIdAsync(accountId);
            return Ok(journeys);
        }
    }
}
