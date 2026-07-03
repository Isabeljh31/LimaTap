using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Cards
{
    /// <summary>
    /// Clase base abstracta para cualquier medio de acceso digital (Token NFC / Wallet).
    /// </summary>
    public abstract class DigitalCard
    {
        public string CardId { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public string TokenId { get; set; } = string.Empty; // ID único detectado por el lector NFC
        public bool IsActive { get; set; } = true;

        // CAMBIO: Pasa de abstract a virtual e incluye un 'set'. 
        // Esto permite a EF Core guardar el tipo de tarjeta, y a las clases hijas sobreescribirlo.
        public virtual string CardType { get; set; } = string.Empty;

        /// <summary>
        /// Aplica las reglas de negocio de descuento sobre la tarifa base. (LSP)
        /// </summary>
        // Mantenemos esto exactamente igual, conservando la pureza de la abstracción
        public abstract decimal CalculateSpecialFare(decimal baseFare);
    }
}