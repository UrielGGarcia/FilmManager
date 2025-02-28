using System.Data;
using FilmManager.Models.Film;
using MySqlConnector;

namespace FilmManager.Core.Services.ClientConnection;

public class FilmService
{
    private readonly string _connectionString;

    public FilmService(string connectionString)
    {
        _connectionString = connectionString;
    }

    //Metodo para obtener las películas de sakila
    public async Task<List<Film>> GetFilms()
    {
        var films = new List<Film>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var query = "SELECT * FROM film";
            using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    Console.WriteLine("Cargando datos...");
                    var rowCount = 0;

                    while (await reader.ReadAsync())
                    {
                        rowCount++;
                        Console.WriteLine($"Cargando film {rowCount}...");

                        try
                        {
                            // Verifica cada columna antes de leerla 
                            var filmId = reader.GetUInt16("film_id");
                            var title = reader.GetString("title");
                            var description = reader.IsDBNull("description") ? null : reader.GetString("description");
                            var releaseYear = reader.GetInt32("release_year");
                            var languageId = reader.GetByte("language_id");
                            var originalLanguageId = reader.IsDBNull("original_language_id")
                                ? (byte?)null
                                : reader.GetByte("original_language_id");
                            var rentalDuration = reader.GetByte("rental_duration");
                            var rentalRate = reader.GetDecimal("rental_rate");
                            var length = reader.IsDBNull("length") ? (ushort?)null : reader.GetUInt16("length");
                            var replacementCost = reader.GetDecimal("replacement_cost");

                            // Error con el enum de rating
                            var ratingValue = reader.GetString("rating");
                            if (!Enum.TryParse(ratingValue.Replace("-", "_"), out Rating parsedRating))
                            {
                                Console.WriteLine($" Valor de ranting inválido '{ratingValue}'");
                                parsedRating = Rating.G; // 🔹 O cualquier valor por defecto
                            }


                            List<string> specialFeatures = reader.IsDBNull("special_features")
                                ? new List<string>()
                                : new List<string>(reader.GetString("special_features").Split(','));
                            var lastUpdate = reader.GetDateTime("last_update");
                            var categoryId = reader.IsDBNull("category_id")
                                ? (byte?)null
                                : reader.GetByte("category_id");

                            // Si llega hasta acá sin errores, agrega la película a la lista
                            films.Add(new Film
                            {
                                FilmId = filmId,
                                Title = title,
                                Description = description,
                                ReleaseYear = releaseYear,
                                LanguageId = languageId,
                                OriginalLanguageId = originalLanguageId,
                                RentalDuration = rentalDuration,
                                RentalRate = rentalRate,
                                Length = length,
                                ReplacementCost = replacementCost,
                                Rating = parsedRating,
                                SpecialFeatures = specialFeatures,
                                LastUpdate = lastUpdate,
                                CategoryId = categoryId
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($" ERROR en fila {rowCount}: {ex.Message}");
                        }
                    }

                    Console.WriteLine($"Leyó {rowCount} películas.");
                }
            }
        }

        Console.WriteLine($"Se recuperaron {films.Count} peliculas.");
        return films;
    }
}