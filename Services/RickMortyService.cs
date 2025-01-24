using rick_morty_webapp.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using rick_morty_webapp.Models;

using static System.Net.WebRequestMethods;
using System.Text.Json;

namespace rick_morty_webapp.Services
{
    public class RickMortyService : IRickMortyService
    {
        private readonly IRickMortyService _rickMortyService;
        private readonly HttpClient _httpClient;
        public RickMortyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Characters> GetCharacter(string characterId)
        {
            try
            {
                // Usa el HttpClient que ya tiene configurada la base address
                var response = await _httpClient.GetAsync($"character/{characterId}");

                // Verifica si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta
                    var content = await response.Content.ReadAsStringAsync();


                    // Deserializa el JSON a tu modelo Characters
                    var character = JsonSerializer.Deserialize<Characters>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return character;
                }

                // Si no encuentra el personaje, devuelve null
                return null;
            }
            catch (Exception ex)
            {
                // Puedes registrar el error si tienes un logger
                Console.WriteLine($"Error fetching character: {ex.Message}");
                return null;
            }

        }
    }
}
