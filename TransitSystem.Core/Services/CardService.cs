using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Cards;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<DigitalCard> RegisterCardAsync(string accountId, string cardType, string tokenId)
        {
            // Esta lógica implementará el Factory Pattern para instanciar tarjetas
            throw new NotImplementedException("Módulo de registro en desarrollo.");
        }

        public async Task<DigitalCard> GetCardDetailsAsync(string tokenId)
        {
            var card = await _cardRepository.GetCardByTokenIdAsync(tokenId);
            if (card == null)
                throw new Exception("Tarjeta no encontrada.");

            return card;
        }

        public async Task<bool> DeactivateCardAsync(string tokenId)
        {
            var card = await _cardRepository.GetCardByTokenIdAsync(tokenId);
            if (card == null) return false;

            card.IsActive = false;
            // La persistencia de este estado se conectará con la infraestructura de base de datos
            return true;
        }
    }
}
