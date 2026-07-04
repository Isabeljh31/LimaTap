using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Interfaces
{
    public interface IPaymentGateway
    {
        /// <summary>
        /// Se conecta con la entidad bancaria para procesar el cobro de la tarjeta de crédito/débito.
        /// </summary>
        /// <param name="paymentToken">Token de seguridad de la tarjeta encriptada.</param>
        /// <param name="amount">Monto a cobrar.</param>
        /// <returns>True si el banco aprueba el pago, False si es rechazado.</returns>
        Task<bool> ProcessPaymentAsync(string paymentToken, decimal amount);
    }
}
