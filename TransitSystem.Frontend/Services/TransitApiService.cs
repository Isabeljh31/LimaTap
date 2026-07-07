using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TransitSystem.Core.Domain.Entities;
using TransitSystem.Shared.Models; 

namespace TransitSystem.Frontend.Services
{
    public class TransitApiService
    {
        private readonly HttpClient _httpClient;

        public TransitApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. Método original: Validación de torniquete
        public async Task<ApiResponse> ValidateTapAsync(TapRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ticketing/validate-tap", request);

            return await response.Content.ReadFromJsonAsync<ApiResponse>()
                   ?? new ApiResponse { Success = false, Message = "Error al leer la respuesta del servidor." };
        }

        // 2. NUEVO: Obtener detalles de la cuenta maestra (para leer el saldo en la vista de Inicio)
        public async Task<UserAccountDto> GetAccountAsync(string accountId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserAccountDto>($"api/Account/{accountId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener la cuenta: {ex.Message}");
                return null;
            }
        }

        // 3. NUEVO: Procesar la recarga web (para el Tab 2)
        public async Task<RechargeResponse> RechargeAsync(RechargeRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Recharge", request);
                response.EnsureSuccessStatusCode(); // Lanza excepción si el código no es 200-299
                return await response.Content.ReadFromJsonAsync<RechargeResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar la recarga: {ex.Message}");
                return new RechargeResponse { Success = false, Message = "Fallo en la comunicación con la pasarela." };
            }
        }

        // 4. NUEVO: Consultar estado de la tarjeta digital (para validar si está activa)
        public async Task<CardStatusDto> GetCardStatusAsync(string tokenId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CardStatusDto>($"api/Card/{tokenId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar la tarjeta: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Journey>> GetJourneyHistoryAsync(string accountId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Journey>>($"api/Journey/{accountId}")
                       ?? new List<Journey>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el historial de viajes: {ex.Message}");
                return new List<Journey>();
            }
        }

        public string GetJourneyExportUrl(
            string accountId,
            DateTime? from = null,
            DateTime? to = null,
            bool includeMetropolitano = true,
            bool includeLinea1 = true)
        {
            var safeAccountId = Uri.EscapeDataString(accountId);
            var parameters = new List<string>();

            if (from is not null)
            {
                parameters.Add($"from={Uri.EscapeDataString(from.Value.ToString("O"))}");
            }

            if (to is not null)
            {
                parameters.Add($"to={Uri.EscapeDataString(to.Value.ToString("O"))}");
            }

            parameters.Add($"includeMetropolitano={includeMetropolitano.ToString().ToLowerInvariant()}");
            parameters.Add($"includeLinea1={includeLinea1.ToString().ToLowerInvariant()}");

            var query = parameters.Count == 0 ? string.Empty : $"?{string.Join("&", parameters)}";
            return new Uri(_httpClient.BaseAddress!, $"api/JourneyExport/{safeAccountId}{query}").ToString();
        }
    }
}
