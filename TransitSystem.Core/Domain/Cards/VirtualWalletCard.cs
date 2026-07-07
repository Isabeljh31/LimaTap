using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransitSystem.Core.Domain.Cards
{
    /// <summary>
    /// Credencial digital generada desde el App Móvil (QR/NFC del celular).
    /// </summary>
    public class VirtualWalletCard : DigitalCard
    {
        public override string CardType => "Virtual_Wallet";

        public override decimal CalculateSpecialFare(decimal baseFare)
        {
            // Podría aplicar promociones futuras por usar el App Oficial
            return baseFare;
        }
    }
}
