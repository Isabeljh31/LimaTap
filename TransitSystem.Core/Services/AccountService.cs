using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.DTOs;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserAccount> RegisterAsync(CreateAccountRequest request)
        {
            var existingAccount = await _accountRepository.GetByIdAsync(request.AccountId);
            if (existingAccount != null)
                throw new Exception("La cuenta ya existe en el sistema.");

            var newAccount = new UserAccount
            {
                AccountId = request.AccountId,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _accountRepository.AddAsync(newAccount);
            return newAccount;
        }

        public async Task<UserAccount> GetAccountDetailsAsync(string accountId)
        {
            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
                throw new Exception("Cuenta no encontrada.");

            return account;
        }
    }
}
