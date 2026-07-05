using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

        // 4. NUEVO: Consultar eastado de la tarjeta digital (para validr si está activa)
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

        // 5. NUEVO: Obtener la información de estaciones para la vista de estaciones
        public async Task<StationsPageDto> GetStationsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<StationsPageDto>("api/Stations");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar estaciones: {ex.Message}");
                return null;
            }
        }
    }
}