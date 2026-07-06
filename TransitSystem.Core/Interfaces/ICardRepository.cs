using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Cards;

namespace TransitSystem.Core.Interfaces
{
    public interface ICardRepository
    {
        Task<DigitalCard> GetCardByTokenIdAsync(string tokenId);
        Task AddCardAsync(DigitalCard card);
    }
}
