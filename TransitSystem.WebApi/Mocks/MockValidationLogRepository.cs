using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.WebApi.Mocks
{
    public class MockValidationLogRepository : IValidationLogRepository
    {
        public Task LogValidationAsync(ValidationEvent validationEvent)
        {
            // Se podría guardar en memoria, pero por simplicidad se ignora.
            return Task.CompletedTask;
        }
    }
}
