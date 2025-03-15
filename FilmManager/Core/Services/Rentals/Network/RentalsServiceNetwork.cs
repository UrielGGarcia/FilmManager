using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
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
    
    //UPDATE
    public static async Task<bool> UpdateRentals(RentalsModel rentals)
    {
        try
        {
            //Serializar el objeto rental a JSON 
            var json = JsonConvert.SerializeObject(rentals);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            //Solicitud PUT al endpoint de update
            var response = await _client.PutAsync(new Uri($"api/rentals/{rentals.RentalId}", UriKind.Relative), content);
            
            //Verifica si la solicitud fue exitosa
            if (response.IsSuccessStatusCode) return true;
            
            //Si la solicitud no fue exitosa, devolverá un mensaje de error 
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error al actualizar rentals: {errorMessage}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar rentals: {ex.Message}");
            return false;
        }
    }
    
}