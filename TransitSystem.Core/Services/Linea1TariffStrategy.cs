using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class Linea1TariffStrategy : ITariffStrategy
    {
        public string SystemType => "Linea1";

        public decimal CalculateFare()
        {
            return 1.50m; // Tarifa base
        }
    }
}
