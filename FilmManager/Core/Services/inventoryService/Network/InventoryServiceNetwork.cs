using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using FilmManager.Models.Inventory;
using Newtonsoft.Json;

namespace FilmManager.Core.Services.inventoryService.Network;

public class InventoryServiceNetwork
{
    private static readonly HttpClient _client = FilmClientHandler.Client;

    // GET
    public static async Task<ObservableCollection<InventoryModel>> GetInventoryByFilmId(int filmId)
    {
        try
        {
            // Crear la ruta
            var url = $"api/inventory?filmId={filmId}";

            // Hacer la petición 
            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<InventoryModel>>(responseContent) ??
                       new ObservableCollection<InventoryModel>();
            }

            MessageBox.Show($"Error al obtener rentas: {response.StatusCode}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Excepeción al obtener rentas:  {ex.Message}   ",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        return new ObservableCollection<InventoryModel>();
    }
}