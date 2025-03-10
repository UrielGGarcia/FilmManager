using System.Collections.ObjectModel;
using FilmManager.Models.Film;

namespace FilmManager.Core.Services.Films;

public interface IFilmService
{
    
    Task<ObservableCollection<Film>> GetListaFilms();
    Task<ObservableCollection<Film>> RemoveFilm(int filmId);

    Film? GetFilmSelected();
    
    void SetFilmSelected(Film film);
    
    Task<ObservableCollection<Film>> UpdateFilm(Film film);

    Task<ObservableCollection<Film>> AddFilm(Film film);



}