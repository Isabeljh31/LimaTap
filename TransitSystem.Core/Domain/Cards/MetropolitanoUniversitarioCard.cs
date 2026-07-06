using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Cards
{
    /// <summary>
    /// Credencial universitaria del Metropolitano para la gestión del beneficio de Medio Pasaje.
    /// </summary>
    public class MetropolitanoUniversitarioCard : DigitalCard
    {
        public override string CardType => "Metropolitano_Universitario";

        /// <summary>
        /// Fecha límite de vigencia de la condición preferencial del carné universitario.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        public override decimal CalculateSpecialFare(decimal baseFare)
        {
            // Validación temporal: Si la credencial expiró, pierde el beneficio automáticamente
            if (DateTime.UtcNow > ExpirationDate)
            {
                return baseFare;
            }

            return baseFare * 0.50m; // Deducción del 50% según la normativa de transporte
        }
    }
}
