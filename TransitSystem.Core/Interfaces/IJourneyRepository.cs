using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;

namespace TransitSystem.Core.Interfaces
{
    public interface IJourneyRepository
    {
        Task<List<Journey>> GetJourneysByAccountIdAsync(string accountId);
    }
}
