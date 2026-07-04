using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Cards;
using TransitSystem.Core.Domain.Entities;

namespace TransitSystem.Core.Interfaces
{
    public interface IAccountRepository
    {
        // Operaciones de la Cuenta ABT
        Task<UserAccount> GetByIdAsync(string accountId);
        Task AddAsync(UserAccount account);
        Task UpdateAsync(UserAccount account); 

    }
}
