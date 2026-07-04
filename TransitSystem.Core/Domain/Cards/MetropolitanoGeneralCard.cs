using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Cards
{
    /// <summary>
    /// Tarjeta estándar del Metropolitano sin descuentos aplicables.
    /// </summary>
    public class MetropolitanoGeneralCard : DigitalCard
    {
        public override string CardType => "Metropolitano_General";

        public override decimal CalculateSpecialFare(decimal baseFare)
        {
            return baseFare; // Consume el 100% de la tarifa regulada base
        }
    }
}
