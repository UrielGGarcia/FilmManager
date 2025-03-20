using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;
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
        return JsonConvert.DeserializeObject<ObservableCollection<Film>>(responseContent) ??
               new ObservableCollection<Film>();
    }


    // Método para eliminar un film
    public static async Task<bool> DeleteFilm(int film)
    {
        try
        {
            var reponse = await _client.DeleteAsync(new Uri($"api/films/{film}", UriKind.Relative));

            if (reponse.IsSuccessStatusCode) return true;

            var errorMessage = await reponse.Content.ReadAsStringAsync();

            MessageBox.Show($"Error: {errorMessage}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            return false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar la renta con id: {ex.Message}",
                "Confirmación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            if (response.IsSuccessStatusCode) return true;

            // Si la solicitud no fue exitosa, mostrará mensaje de error 
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error al actualizar el film:  {errorMessage}");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción al actualizar la película: {ex.Message}");
            return false;
        }
    }


    // Método para crear
    public static async Task<bool> AddFilm(Film film)
    {
        try
        {
            // Serializar el objeto FIlm a Json
            // Serializar solo las propiedades deseadas 
            var json = JsonConvert.SerializeObject(new
            {
                film.Title,
                film.ReleaseYear,
                film.LanguageId,
                film.Description,
                film.RentalDuration,
                film.RentalRate,
                film.Length,
                film.ReplacementCost,
                film.Rating,
                film.SpecialFeatures
            });

            Console.WriteLine(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Solicitud edpoint Post
            var response = await _client.PostAsync(new Uri("api/films", UriKind.Relative), content);

            // Verifica si la solicitud fue exitosa
            if (response.IsSuccessStatusCode) return true;

            // La respuesta no fue exitosa y mostrará error
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