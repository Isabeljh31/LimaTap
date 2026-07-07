using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Shared.Models
{
    public class RechargeRequest
    {
        public string AccountId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentToken { get; set; } = string.Empty; // Simulador del token de la tarjeta (Visa/Mastercard/Yape)
    }
}
