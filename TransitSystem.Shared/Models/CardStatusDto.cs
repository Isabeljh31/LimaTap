    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Shared.Models
{
    public class CardStatusDto
    {
        public string TokenId { get; set; } = string.Empty;
        public string CardType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
