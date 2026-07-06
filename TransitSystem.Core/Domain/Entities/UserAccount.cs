using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Enums;

namespace TransitSystem.Core.Domain.Entities
{
    /// <summary>
    /// Representa la cuenta centralizada del usuario (Account-Based Ticketing).
    /// </summary>
    public class UserAccount
    {
        public string AccountId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Propiedad protegida: Nadie puede asignarle valor desde afuera
        public decimal Balance { get; private set; }

        public AccountStatus Status { get; set; } = AccountStatus.Active;

        // MÉTODOS DE COMPORTAMIENTO (SOLID / DDD)  

        public void AddFunds(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto a recargar debe ser mayor a cero.");

            Balance += amount;
        }

        public bool DeductFunds(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("El monto a deducir debe ser mayor a cero.");

            if (Balance < amount)
                return false; // Retorna falso si no hay saldo suficiente (evita que el sistema caiga)

            Balance -= amount;
            return true; // Retorna verdadero si el cobro fue exitoso
        }
    }
}
