using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.Navigate;
using FilmManager.Models.Film;
using FilmManager.ViewMovie;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.ViewCreate;

public partial class ViewCreateViewModel : ObservableObject
{
    private readonly IFilmService _filmService = App.Current.Services.GetService<IFilmService>()!;

    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;

    [ObservableProperty] private Film? _film;

    [ObservableProperty] private ObservableCollection<Film> _films = new(); // Inicializar la colección

    [ObservableProperty] private bool _isTextBoxEnabled;

    public ViewCreateViewModel()
    {
        Film = new Film();
    }


    public string SpecialFeaturesText
    {
        set => _film.SpecialFeatures =
            value.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    [RelayCommand]
    private void OnCreateFilm()
    {
        if (Film == null) return;

        Films.Add(Film);

        // Obtener el último film agregado 
        var filmSelected = Films.LastOrDefault();

        if (filmSelected != null)
        {
            var result = MessageBox.Show("¿Está seguro que desea guardar el film creado?",
                "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.OK)
            {
                _filmService.AddFilm(filmSelected);
                MessageBox.Show($"El film con nombre {filmSelected.Title} fuer guardado correctamente-",
                    "Confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.Navigate(typeof(ViewMoviePage));
            }
        }
    }
}