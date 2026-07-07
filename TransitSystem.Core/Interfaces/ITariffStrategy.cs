using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Interfaces
{
    public interface ITariffStrategy
    {
        string SystemType { get; }
        decimal CalculateFare();
    }
}
