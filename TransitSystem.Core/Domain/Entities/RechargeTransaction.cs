using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Enums;

namespace TransitSystem.Core.Domain.Entities
{
    public class RechargeTransaction
    {
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string PaymentMethod { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; } = TransactionStatus.Completed;
    }
}
