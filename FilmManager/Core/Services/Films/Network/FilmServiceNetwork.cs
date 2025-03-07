using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Models.Film;
using Newtonsoft.Json;

namespace FilmManager.Core.Services.Films.Network;

public class FilmServiceNetwork
{
    private static readonly HttpClient _client = FilmClientHandler.Client;

    // Método para obtener los registros
    public static async Task<ObservableCollection<Film>> GetFilms()
    {
        var response = await _client.GetAsync(new Uri("api/films", UriKind.Relative));
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        return JsonConvert.DeserializeObject<ObservableCollection<Film>>(responseContent) ?? new ObservableCollection<Film>();
    }
    
    
    // Método para eliminar un film
    public static async Task<bool> DeleteFilm(int film)
    {
        try
        {
            var reponse = await _client.DeleteAsync(new Uri($"api/films/{film}", UriKind.Relative));

            if (reponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMessage = await reponse.Content.ReadAsStringAsync();
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al eliminar la película:  {ex.Message}");
            return false;
        }
    }
    
    // Método para actualizar
    public static async Task<bool> UpdateFilm(Film film)
    {
        try
        {
            //Serializar el objeto Film a Json
            var json = JsonConvert.SerializeObject(film);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Solicitud PUT al edpoint de update
            var response = await _client.PutAsync(new Uri($"api/films/{film.FilmId}", UriKind.Relative), content);

            // Verifica si la solicitud fue exitosa
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                // Si la solicitud no fue exitosa, mostrará mensaje de error 
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al actualizar el film:  {errorMessage}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al actualizar la película: {ex.Message}");
            return false;
        }
    }
    
}