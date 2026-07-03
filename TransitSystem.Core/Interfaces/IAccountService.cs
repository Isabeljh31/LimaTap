using System;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.DTOs;

namespace TransitSystem.Core.Interfaces // <-- Esta línea es crucial
{
    public interface IAccountService
    {
        Task<UserAccount> RegisterAsync(CreateAccountRequest request);
        Task<UserAccount> GetAccountDetailsAsync(string accountId);
    }
}