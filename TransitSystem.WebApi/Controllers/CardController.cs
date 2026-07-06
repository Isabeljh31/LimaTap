using Microsoft.AspNetCore.Mvc;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService) => _cardService = cardService;

        [HttpGet("{tokenId}")]
        public async Task<IActionResult> GetStatus(string tokenId)
        {
            var card = await _cardService.GetCardDetailsAsync(tokenId);
            return Ok(new
            {
                card.TokenId,
                card.CardType,
                card.IsActive
            });
        }
    }
}
