using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitSystem.Core.Domain.Cards;
using TransitSystem.Core.Interfaces;
using TransitSystem.Infrastructure.Data;

namespace TransitSystem.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;
        public CardRepository(ApplicationDbContext context) => _context = context;

        public async Task<DigitalCard> GetCardByTokenIdAsync(string tokenId)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.TokenId == tokenId);
        }

        public async Task AddCardAsync(DigitalCard card)
        {
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
        }
    }
}