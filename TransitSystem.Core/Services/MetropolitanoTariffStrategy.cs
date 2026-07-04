using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class MetropolitanoTariffStrategy : ITariffStrategy
    {
        public string SystemType => "Metropolitano";

        public decimal CalculateFare()
        {
            return 3.20m; // Tarifa base (sin descuentos aplicados aún)
        }
    }
}
