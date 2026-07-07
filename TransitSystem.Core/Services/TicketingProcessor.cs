using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransitSystem.Core.Interfaces;
using TransitSystem.Core.Domain.Cards;    
using TransitSystem.Core.Domain.Entities; 
using TransitSystem.Core.Domain.Enums;    

namespace TransitSystem.Core.Services
{
    public class TicketingProcessor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository; // 1. Agregado el repositorio de tarjetas
        private readonly IValidationLogRepository _validationLogRepository;
        private readonly IEnumerable<ITariffStrategy> _tariffStrategies;

        public TicketingProcessor(
            IAccountRepository accountRepository,
            ICardRepository cardRepository, // 2. Inyectado en el constructor
            IValidationLogRepository validationLogRepository,
            IEnumerable<ITariffStrategy> tariffStrategies)
        {
            _accountRepository = accountRepository;
            _cardRepository = cardRepository; // 3. Asignado
            _validationLogRepository = validationLogRepository;
            _tariffStrategies = tariffStrategies;
        }

        public async Task<bool> ProcessTapInAsync(string tokenId, string systemType, string stationId)
        {
            // 1. Identificar la tarjeta por el Token NFC usando el repositorio correcto
            var card = await _cardRepository.GetCardByTokenIdAsync(tokenId);
            if (card == null || !card.IsActive) return false;

            // 2. Obtener la cuenta maestra (Modelo ABT)
            var account = await _accountRepository.GetByIdAsync(card.AccountId);

            if (account == null || account.Status != AccountStatus.Active) return false;

            // 3. Seleccionar la tarifa del sistema (Metropolitano o Línea 1)
            var normalizedSystemType = NormalizeSystemType(systemType);
            var strategy = _tariffStrategies.FirstOrDefault(s =>
                string.Equals(NormalizeSystemType(s.SystemType), normalizedSystemType, StringComparison.OrdinalIgnoreCase));

            if (strategy == null) return false;

            decimal baseFare = strategy.CalculateFare();

            // 4. Aplicar descuento polimórfico si es tarjeta universitaria/escolar
            decimal finalFare = card.CalculateSpecialFare(baseFare);

            // 5. Y 6. Ejecutar el cobro de forma segura (El dominio toma la decisión)
            bool paymentSuccessful = account.DeductFunds(finalFare);

            if (!paymentSuccessful) return false; 

           // 6. Guardar los cambios en la cuenta
            await _accountRepository.UpdateAsync(account);

            // 7. Guardar el log de la transacción
            var valEvent = new ValidationEvent
            {
                TokenId = tokenId,
                StationId = stationId
            };
            await _validationLogRepository.LogValidationAsync(valEvent);

            return true;
        }

        private static string NormalizeSystemType(string systemType)
        {
            return (systemType ?? string.Empty).Trim().Replace(" ", string.Empty, StringComparison.OrdinalIgnoreCase);
        }
    }
}