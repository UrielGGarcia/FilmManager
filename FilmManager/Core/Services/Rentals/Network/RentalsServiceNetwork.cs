using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
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

            MessageBox.Show($"Error al obtener rentas: {response.StatusCode}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            return new Response<ObservableCollection<RentalsModel>>
            {
                Data = new ObservableCollection<RentalsModel>(),
                TotalPages = 0
            };
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Excepeción al obtener rentas:  {ex.Message}   ",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            return new Response<ObservableCollection<RentalsModel>>
            {
                Data = new ObservableCollection<RentalsModel>(),
                TotalPages = 0
            };
        }
    }

    //UPDATE
    public static async Task<bool> UpdateRental(RentalsModel rental)
    {
        try
        {
            // Serializar el objeto Rental a Json
            var json = JsonConvert.SerializeObject(rental);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Solicitud PUT al edpoint de update
            var response = await _client.PutAsync(new Uri($"api/rental/{rental.RentalId}", UriKind.Relative), content);

            // Verifica si la solitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"La renta con id: {rental.RentalId} fue modificado correctamente.",
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return true;
            }

            // Si la solicitud no fue exitosa, mostrará mensaje de error
            var errorMessage = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"Error al actualizar la renta con id: {errorMessage}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"La renta con id: {rental.RentalId} Tuvo una excepción : {ex.Message}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return false;
        }
    }

    // Método de eliminación 

    public static async Task<bool> DeleteRental(int filmId)
    {
        try
        {
            var response = await _client.DeleteAsync(new Uri($"api/rental/{filmId}", UriKind.Relative));


            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"La renta con id: {filmId} fue eliminada correctamente.",
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return true;
            }


            var errorMessage = await response.Content.ReadAsStringAsync();
            MessageBox.Show($"Error: {errorMessage}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Excepción:  {ex.Message}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return false;
        }
    }

    // POST 
    public static async Task<bool> AddRental(RentalsModel rental)
    {
        try
        {
            var json = JsonConvert.SerializeObject(rental);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(new Uri("api/rental", UriKind.Relative), content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Se creó exitosamente la renta.",
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            var errorMessage = await response.Content.ReadAsStringAsync();

            MessageBox.Show($"Error: {errorMessage}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            return false;
        }
    }
}