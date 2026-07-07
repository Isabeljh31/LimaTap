using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Exceptions
{
    public class CardExpiredException : Exception
    {
        public CardExpiredException(string message) : base(message) { }
    }
}
