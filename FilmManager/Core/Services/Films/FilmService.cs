using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Network;
using FilmManager.Models.Film;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Core.Services.Films;

public class FilmService : IFilmService
{
    //Declaración de lista vacía
    private ObservableCollection<Film> _films =  new();

    private Film? _selectedFilm;
    private bool _filmsTF;
    private bool _filmsUP;
    private IFilmService _filmServiceImplementation;
    private FilmServiceNetwork? _filmServiceNetwork = App.Current.Services.GetService<FilmServiceNetwork>();

    public void AddFilm(Film film)
    {
    }


    public async Task<ObservableCollection<Film>> RemoveFilm(int filmId)
    {
        _filmsTF = await FilmServiceNetwork.DeleteFilm(filmId);
        return _films;
    }


    public async Task<ObservableCollection<Film>> UpdateFilm(Film film)
    {
        var index = _films.IndexOf(film);
        if (index != 0) _films[index] = film;
        
        _filmsTF = await FilmServiceNetwork.UpdateFilm(film);
        return _films;
    }

    public async Task<ObservableCollection<Film>> GetListaFilms()
    {
        _films = await FilmServiceNetwork.GetFilms(); 
        return _films;
    }

    public Film? GetFilmSelected()
    {
        return _selectedFilm;
    }

    public void SetFilmSelected(Film film)
    {
        _selectedFilm = film;
    }
    
}