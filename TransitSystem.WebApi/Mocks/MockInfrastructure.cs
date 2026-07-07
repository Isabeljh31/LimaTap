using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public class MockJourneyRepository : IJourneyRepository
    {
        private static readonly List<Journey> Journeys = BuildJourneys();

        public Task<List<Journey>> GetJourneysByAccountIdAsync(string accountId)
        {
            var journeys = Journeys
                .Where(journey => journey.AccountId == accountId)
                .OrderByDescending(journey => journey.StartTime)
                .ToList();

            return Task.FromResult(journeys);
        }

        private static List<Journey> BuildJourneys()
        {
            var today = DateTime.Today;
            var lastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            return new List<Journey>
            {
                Create("journey-api-001", "Naranjal", "Estación Central", 2.70m, today.AddHours(7).AddMinutes(42), 36),
                Create("journey-api-002", "Estación Central", "Atocongo", 0m, today.AddHours(8).AddMinutes(23), 25),
                Create("journey-api-003", "Javier Prado", "Comas", 3.20m, today.AddDays(-1).AddHours(18).AddMinutes(10), 52),
                Create("journey-api-004", "San Borja Norte", "Villa El Salvador", 1.50m, today.AddDays(-2).AddHours(7).AddMinutes(5), 32),
                Create("journey-api-005", "Matellini", "Angamos", 2.70m, today.AddDays(-3).AddHours(12).AddMinutes(20), 31),
                Create("journey-api-006", "Angamos", "La Cultura", 0m, today.AddDays(-3).AddHours(13).AddMinutes(2), 13),
                Create("journey-api-007", "Naranjal", "Canaval y Moreyra", 3.20m, today.AddDays(-6).AddHours(8).AddMinutes(4), 37),
                Create("journey-api-008", "Bayóvar", "Miguel Grau", 1.50m, today.AddDays(-8).AddHours(9).AddMinutes(18), 35),
                Create("journey-api-009", "Tomás Valle", "Plaza de Flores", 2.70m, today.AddDays(-12).AddHours(17).AddMinutes(25), 39),
                Create("journey-api-010", "Gamarra", "Cabitos", 1.50m, today.AddDays(-15).AddHours(6).AddMinutes(50), 29),
                Create("journey-api-011", "UNI", "Estadio Nacional", 2.70m, today.AddDays(-21).AddHours(19).AddMinutes(12), 33),
                Create("journey-api-012", "Naranjal", "Estación Central", 2.70m, lastMonth.AddDays(4).AddHours(7).AddMinutes(35), 37),
                Create("journey-api-013", "Estación Central", "La Cultura", 0m, lastMonth.AddDays(4).AddHours(8).AddMinutes(18), 22),
                Create("journey-api-014", "Villa El Salvador", "Naranjal", 3.20m, lastMonth.AddDays(11).AddHours(18).AddMinutes(6), 56),
                Create("journey-api-015", "Atocongo", "Bayóvar", 1.50m, lastMonth.AddDays(18).AddHours(10).AddMinutes(20), 45),
                Create("journey-api-016", "Matellini", "Naranjal", 3.20m, lastMonth.AddDays(24).AddHours(7).AddMinutes(55), 48)
            };
        }

        private static Journey Create(string id, string origin, string destination, decimal fare, DateTime startTime, int durationMinutes) =>
            new Journey
            {
                JourneyId = id,
                AccountId = "12345",
                OriginStationId = origin,
                DestinationStationId = destination,
                FareApplied = fare,
                StartTime = startTime,
                EndTime = startTime.AddMinutes(durationMinutes)
            };
    }
}
