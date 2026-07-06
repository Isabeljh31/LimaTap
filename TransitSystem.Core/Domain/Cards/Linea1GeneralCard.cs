using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Cards
{
    /// <summary>
    /// Tarjeta estándar de la Línea 1 del Metro de Lima.
    /// </summary>
    public class Linea1GeneralCard : DigitalCard
    {
        public override string CardType { get; set; } = "Linea1General";

        public override decimal CalculateSpecialFare(decimal baseFare)
        {
            return baseFare; // La Línea 1 general paga el 100% (S/ 1.50)
        }
    }
}
