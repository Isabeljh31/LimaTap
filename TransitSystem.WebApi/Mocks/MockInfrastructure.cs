using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Domain.Cards;
using TransitSystem.Core.Domain.Enums;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Mocks
{
    public class MockRechargeRepository : IRechargeTransactionRepository
    {
        public Task AddAsync(RechargeTransaction transaction) => Task.CompletedTask;
        public Task<RechargeTransaction> GetByIdAsync(string transactionId) => Task.FromResult(new RechargeTransaction());
    }

    public class MockPaymentGateway : IPaymentGateway
    {
        public Task<bool> ProcessPaymentAsync(string paymentToken, decimal amount) => Task.FromResult(true);
    }

    public class MockAccountRepository : IAccountRepository
    {
        // El 'static' asegura que el saldo viva en la RAM mientras la API esté encendida
        private static readonly UserAccount _mockAccount = new UserAccount
        {
            AccountId = "12345",
            FirstName = "Carlos",
            LastName = "López",
            Status = AccountStatus.Active
        };

        public MockAccountRepository()
        {
            // Inicializa en 42.50 la primera vez
            if (_mockAccount.Balance == 0) _mockAccount.AddFunds(42.50m);
        }

        public Task<UserAccount> GetByIdAsync(string accountId) => Task.FromResult(accountId == "12345" ? _mockAccount : null!);
        public Task AddAsync(UserAccount account) => Task.CompletedTask;
        public Task UpdateAsync(UserAccount account) => Task.CompletedTask;
    }

    public class MockCardRepository : ICardRepository
    {
        public Task<DigitalCard> GetCardByTokenIdAsync(string tokenId)
        {
            return Task.FromResult<DigitalCard>(new Linea1GeneralCard
            {
                TokenId = tokenId,
                AccountId = "12345",
                IsActive = true
            });
        }
        public Task AddCardAsync(DigitalCard card) => Task.CompletedTask;
    }
}