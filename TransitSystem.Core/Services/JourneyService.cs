using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IJourneyRepository _journeyRepository;

        public JourneyService(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        public async Task<List<Journey>> GetJourneyHistoryAsync(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentException("El accountId es requerido");
            }

            return await _journeyRepository
                .GetJourneysByAccountIdAsync(accountId);
        }
    }
}
