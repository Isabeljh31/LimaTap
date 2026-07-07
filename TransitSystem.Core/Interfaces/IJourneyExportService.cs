using System.Threading.Tasks;
using TransitSystem.Core.DTOs;

namespace TransitSystem.Core.Interfaces
{
    public interface IJourneyExportService
    {
        Task<byte[]> ExportAsync(JourneyExportOptions options);
    }
}
