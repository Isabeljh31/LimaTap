using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;


namespace TransitSystem.Core.Interfaces
{
    public interface IValidationLogRepository
    {
        Task LogValidationAsync(ValidationEvent validationEvent);
    }
}
