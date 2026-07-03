using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Cards;

namespace TransitSystem.Core.Interfaces
{
    public interface ICardService
    {
        // Agregamos este contrato necesario para el controlador
        Task<DigitalCard> GetCardDetailsAsync(string tokenId);

        Task<DigitalCard> RegisterCardAsync(string accountId, string cardType, string tokenId);
        Task<bool> DeactivateCardAsync(string tokenId);
    }
}

