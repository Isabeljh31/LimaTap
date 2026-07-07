using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Interfaces;
using TransitSystem.Infrastructure.Data;

namespace TransitSystem.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context) => _context = context;

        public async Task<UserAccount> GetByIdAsync(string accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task AddAsync(UserAccount account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserAccount account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}