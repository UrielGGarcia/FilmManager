using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.Navigate;
using FilmManager.Core.Services.Rentals;
using FilmManager.Features.Rentals.UpdateRentals;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FilmManager.Features.Rentals.ViewRentals;

public partial class ViewRentalsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;
    private readonly IRentalsService _rentalsService = App.Current.Services.GetService<IRentalsService>()!;
    [ObservableProperty] private int _actuallPage;
    [ObservableProperty] private bool _isEnabledNext;
    [ObservableProperty] private bool _isEnablePrevious;

    [ObservableProperty] private RentalsModel _rental;
    [ObservableProperty] private ObservableCollection<RentalsModel> _rentals;
    [ObservableProperty] private int _totalPages;


    public ViewRentalsViewModel()
    {
        IsEnablePrevious = false;
        IsEnabledNext = true;
        _actuallPage = 1;
        LoadAsync(ActuallPage);
        IsEnablePrevious = false;
    }

    private async Task LoadAsync(int page)
    {
        var response = await _rentalsService.GetListaRentals(page);
        Rentals = response.Data; // Asignar la colección de rentals
        TotalPages = response.TotalPages; // Asignar el valor de TotalPages
        EnableNext();
        EnablePrevious();
    }

    [RelayCommand]
    public void OnPreviousPage()
    {
        if (ActuallPage > 1)
        {
            ActuallPage--;
            LoadAsync(ActuallPage);
            Console.WriteLine(ActuallPage);
        }
    }

    [RelayCommand]
    public void OnNextPage()
    {
        if (ActuallPage >= 1 && ActuallPage < TotalPages)
        {
            ActuallPage++;
            LoadAsync(ActuallPage);
            Console.WriteLine(ActuallPage);
        }
    }


    public void EnablePrevious()
    {
        if (ActuallPage <= 1)
            IsEnablePrevious = false;
        else
            IsEnablePrevious = true;
    }

    public void EnableNext()
    {
        if (ActuallPage >= TotalPages)
            IsEnabledNext = false;
        else
            IsEnabledNext = true;
    }

    //AGREGAR EL RELAY COMMAND

    [RelayCommand]
    private void OnRentalClick()
    {
        if (Rental != null)
        {
            var json = JsonConvert.SerializeObject(Rental);
            Console.WriteLine($"Renta seleccionada: {json}");

            // Establece la renta seleccionada para el servicio
            _rentalsService.SetRentalsSelected(Rental);

            // Navegar a la página update y eliminación
            _navigationService.Navigate(typeof(UpdateRentalPage));
        }
        else
        {
            Console.WriteLine("No se seleccionó ninguna renta.");
        }
    }
}