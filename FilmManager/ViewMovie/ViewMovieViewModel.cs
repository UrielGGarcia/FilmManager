using System.Collections.ObjectModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.Navigate;
using FilmManager.Models.Film;
using FilmManager.ViewUpdate;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.ViewMovie;

public partial class ViewMovieViewModel : ObservableObject


{
    [ObservableProperty] private ObservableCollection<Film> _films;
    [ObservableProperty] private Film _film;
    private readonly IFilmService _filmService = App.Current.Services.GetService<IFilmService>()!;
    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;

    public ViewMovieViewModel()
    {
        LoadAsync();
    }
    
    
    private async Task LoadAsync()
    {
        Films = await _filmService.GetListaFilms();
    }

    [RelayCommand]
    private void OnFilmClick()
    {
        if (_film != null)
        {
            // Establece el film seleccionado en el servico
            _filmService.SetFilmSelected(_film);
            
            //Navegar a la página de update y eliminación
            _navigationService.Navigate(typeof(ViewUpdateMoviePage));
        }
    }
    
    
}