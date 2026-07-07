using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyRepository _journeyRepository;

        public JourneyController(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetHistory(string accountId)
        {
            var journeys = await _journeyRepository.GetJourneysByAccountIdAsync(accountId);
            return Ok(journeys);
        }
    }
}
