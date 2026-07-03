using System;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Domain.Enums;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class RechargeService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRechargeTransactionRepository _transactionRepository;
        private readonly IPaymentGateway _paymentGateway;

        public RechargeService(
            IAccountRepository accountRepository,
            IRechargeTransactionRepository transactionRepository,
            IPaymentGateway paymentGateway)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _paymentGateway = paymentGateway;
        }

        public async Task<RechargeTransaction> ProcessWebRechargeAsync(string accountId, decimal amount, string paymentToken, string paymentMethod)
        {
            if (amount <= 0) throw new ArgumentException("El monto a recargar debe ser mayor a cero.");

            var account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null) throw new ArgumentException("Cuenta no encontrada.");

            // 1. Crear el registro de la transacción en estado Pendiente
            var transaction = new RechargeTransaction
            {
                AccountId = accountId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = TransactionStatus.Pending
            };

            // Lo guardamos inicialmente como pendiente por si la pasarela tarda o falla
            await _transactionRepository.AddAsync(transaction);

            // 2. Procesar el pago con la pasarela (Niubiz/Stripe)
            bool paymentSuccess = await _paymentGateway.ProcessPaymentAsync(paymentToken, amount);

            if (!paymentSuccess)
            {
                transaction.Status = TransactionStatus.Failed;
                // Si la pasarela falla, el flujo termina aquí.
                throw new Exception("El pago fue rechazado por la entidad bancaria.");
            }

            // 3. Si el pago es exitoso, actualizamos el dominio
            transaction.Status = TransactionStatus.Completed;
            account.AddFunds(amount); // Usamos el método seguro de la entidad

            // 4. Persistir los cambios en la cuenta
            await _accountRepository.UpdateAsync(account);

            return transaction;
        }
    }
}