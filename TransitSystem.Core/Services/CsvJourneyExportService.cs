using System.Globalization;
using System.Text;
using TransitSystem.Core.DTOs;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Core.Interfaces;

namespace TransitSystem.Core.Services
{
    public class CsvJourneyExportService : IJourneyExportService
    {
        private readonly IJourneyRepository _journeyRepository;

        public CsvJourneyExportService(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        public async Task<byte[]> ExportAsync(JourneyExportOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.AccountId))
            {
                throw new ArgumentException("El accountId es requerido");
            }

            var journeys = await _journeyRepository.GetJourneysByAccountIdAsync(options.AccountId);
            var csv = new StringBuilder();

            csv.AppendLine("Id,Cuenta,Linea,Origen,Destino,Fecha inicio,Fecha fin,Tarifa");

            foreach (var journey in FilterJourneys(journeys, options).OrderByDescending(journey => journey.StartTime))
            {
                var lineName = ResolveLineName(journey);

                csv.AppendLine(string.Join(",",
                    Escape(journey.JourneyId),
                    Escape(journey.AccountId),
                    Escape(lineName),
                    Escape(journey.OriginStationId),
                    Escape(journey.DestinationStationId),
                    Escape(journey.StartTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)),
                    Escape(FormatEndTime(journey)),
                    journey.FareApplied.ToString("0.00", CultureInfo.InvariantCulture)));
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private static IEnumerable<Journey> FilterJourneys(IEnumerable<Journey> journeys, JourneyExportOptions options) =>
            journeys
                .Where(journey => options.From is null || journey.StartTime >= options.From.Value)
                .Where(journey => options.To is null || journey.StartTime < options.To.Value)
                .Where(journey => IsIncludedLine(journey, options));

        private static bool IsIncludedLine(Journey journey, JourneyExportOptions options)
        {
            var lineName = ResolveLineName(journey);

            return lineName switch
            {
                "Metropolitano" => options.IncludeMetropolitano,
                "Linea 1" => options.IncludeLinea1,
                _ => true
            };
        }

        private static string ResolveLineName(Journey journey)
        {
            string[] linea1Stations =
            {
                "Atocongo", "Bayovar", "Bayóvar", "Cabitos", "Gamarra", "La Cultura", "Miguel Grau",
                "San Borja Norte", "Villa El Salvador"
            };

            return linea1Stations.Contains(journey.OriginStationId) || linea1Stations.Contains(journey.DestinationStationId)
                ? "Linea 1"
                : "Metropolitano";
        }

        private static string FormatEndTime(Journey journey) =>
            journey.EndTime?.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) ?? string.Empty;

        private static string Escape(string value)
        {
            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
    }
}
