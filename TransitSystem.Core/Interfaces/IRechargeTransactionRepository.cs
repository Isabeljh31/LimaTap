using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;

namespace TransitSystem.Core.Interfaces
{
    public interface IRechargeTransactionRepository
    {
        /// <summary>
        /// Guarda el registro de una nueva transacción de recarga (Pendiente, Completada o Fallida).
        /// </summary>
        Task AddAsync(RechargeTransaction transaction);

        /// <summary>
        /// Obtiene una transacción específica por su ID de operación.
        /// </summary>
        Task<RechargeTransaction> GetByIdAsync(string transactionId);
    }
}