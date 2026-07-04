using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.DTOs
{
    public class RechargeRequest
    {
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentToken { get; set; } = string.Empty;
    }
}
