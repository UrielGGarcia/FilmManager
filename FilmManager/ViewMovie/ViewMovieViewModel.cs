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
   

    // Constructor principal
    public ViewMovieViewModel()
    {
        _filmService = new FilmService();
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