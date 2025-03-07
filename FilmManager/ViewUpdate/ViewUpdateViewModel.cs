using System.Net.Http;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.Films;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Core.Services.Navigate;
using FilmManager.Models.Film;
using FilmManager.ViewMovie;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FilmManager.ViewUpdate;

public partial class ViewUpdateViewModel : ObservableObject
{
    [ObservableProperty] private Film? _film;
    private readonly IFilmService _filmService = App.Current.Services.GetService<IFilmService>()!;
    [ObservableProperty] private bool _isTextBoxEnabled;
    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;
    private string _selectedSpecialFeature;

    public ViewUpdateViewModel()
    {
        Film = _filmService.GetFilmSelected();
        _isTextBoxEnabled = false;
        
    }
    

    [RelayCommand]
    private void OnToggleEdit()
    {
        IsTextBoxEnabled = true;
    }
    

    [RelayCommand]
    public void OnEliminarFilm()
    {
        var filmSeleccionado = _filmService.GetFilmSelected();

        if (filmSeleccionado != null)
        {
            var result = MessageBox.Show($"¿Está seguro que desea eliminar al film con nombre: {filmSeleccionado.Title}?",
                "Confirmación",MessageBoxButton.OKCancel,MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.OK)
            {
                _filmService.RemoveFilm(filmSeleccionado.FilmId);
                MessageBox.Show($"El film con nombre: {filmSeleccionado.Title} fue eliminado correctamente.",
                    "Confirmación",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                _navigationService.Navigate(typeof(ViewMoviePage));
            }
                    
        }
    }
    
    // Agrega lo que ya está agregado en el campo, al combobox
    public string SelectedSpecialFeature
    {
        get => _selectedSpecialFeature;
        set
        {
            if (value != null && !_film.SpecialFeatures.Contains(value))
            {
                _film.SpecialFeatures.Add(value);
                OnPropertyChanged(nameof(SpecialFeaturesText)); // Notifica cambio
                OnPropertyChanged(nameof(AvailableSpecialFeaturesFiltered)); // Actualiza el ComboBox
            }
            _selectedSpecialFeature = null; // Limpia el ComboBox después de seleccionar
            OnPropertyChanged();
        }
    }
    
    //Convertir string a lista y vicerversa
    public string SpecialFeaturesText 
    {
        get => _film.SpecialFeatures != null ? string.Join(", ", _film.SpecialFeatures) : string.Empty;
        set => _film.SpecialFeatures = value.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    
    // Lista de lo que permite el campo SpecialFeatures
    public List<string> AvailableSpecialFeatures { get; } = new List<string> 
    { 
        "Trailers", "Deleted Scenes", "Behind the Scenes", "Commentaries"
    };

    
    // Filtrado de lo que ya está "seleccionado"
    public List<string> AvailableSpecialFeaturesFiltered
    {
        get => AvailableSpecialFeatures
            .Where(feature => !_film.SpecialFeatures.Contains(feature))
            .ToList();
    }


    [RelayCommand]
    public void OnGuardarFilm()
    {
        var filmSeleccionado = _filmService.GetFilmSelected();

        if (filmSeleccionado != null)
        {
            var result = MessageBox.Show($"¿Está seguro que desea modificar al film de id: {filmSeleccionado.FilmId}?",
                "Confirmación",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            
            if (result == MessageBoxResult.OK)
            {
                _filmService.UpdateFilm(filmSeleccionado);
                MessageBox.Show($"El film con id: {filmSeleccionado.FilmId} fue modificado correctamente.",
                    "Confirmación",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                _navigationService.Navigate(typeof(ViewMoviePage));
            }
        }
        else
        {
            MessageBox.Show("No se encontro el film.");
        }
    }
    
}