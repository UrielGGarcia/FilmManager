using System.Collections.ObjectModel;
using System.Net.Http;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Models.Rentals;
using Newtonsoft.Json;

namespace FilmManager.Core.Services.Rentals.Network;

public class RentalsServiceNetwork
{
    private static readonly HttpClient _client = FilmClientHandler.Client;

    // Método para obtener ls registros
    public static async Task<Response<ObservableCollection<RentalsModel>>> GetRentalsAsync(int page, int pageSize)
    {
        try
        {
            // Crear la ruta con los query parámetros
            var url = $"api/rental?page={page}&pageSize={pageSize}";
            // Hacer la petición
            var response = await _client.GetAsync(new Uri(url, UriKind.Relative));


            // Veririfcar el estado de la respuesta para manejar errores
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                // Deserializar la respuesta en un objeto Response<ObservableCollection<RentalsModel>>
                var responseObject =
                    JsonConvert.DeserializeObject<Response<ObservableCollection<RentalsModel>>>(responseContent);

                // Devolver el objeto Response
                return responseObject ?? new Response<ObservableCollection<RentalsModel>>
                {
                    Data = new ObservableCollection<RentalsModel>(),
                    TotalPages = 0
                };
            }

            Console.WriteLine($"Error al obtener rentals: {response.StatusCode}");
            return new Response<ObservableCollection<RentalsModel>>
            {
                Data = new ObservableCollection<RentalsModel>(),
                TotalPages = 0
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al obtener rentals: {ex.Message}");
            return new Response<ObservableCollection<RentalsModel>>
            {
                Data = new ObservableCollection<RentalsModel>(),
                TotalPages = 0
            };
        }
    }
}