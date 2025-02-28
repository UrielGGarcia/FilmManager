using System.Collections.ObjectModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using FilmManager.Core.Services.ClientConnection;
using FilmManager.Models.Film;

namespace FilmManager.ViewMovie;

public class ViewMovieViewModel : ObservableObject
{
    private readonly FilmService _filmService;

    // Constructor sin parámetros para XAML
    public ViewMovieViewModel() : this(
        "Server=192.168.56.20;Port=3306;Database=sakila;User=uriel;Password=uriel.120;SslMode=None;")
    {
    }

    // Constructor principal
    public ViewMovieViewModel(string connectionString)
    {
        _filmService = new FilmService(connectionString);
        Films = new ObservableCollection<Film>();
    }

    public ObservableCollection<Film> Films { get; set; } = new();

    public async Task LoadFilms()
    {
        var filmsList = await _filmService.GetFilms();
        Films.Clear();
        foreach (var film in filmsList)
        {
            Films.Add(film);
            CollectionViewSource.GetDefaultView(Films)?.Refresh();
        }
    }
}