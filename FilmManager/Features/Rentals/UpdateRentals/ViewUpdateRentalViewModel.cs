using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmManager.Core.Services.Rentals;
using FilmManager.Features.Rentals.ViewRentals;
using FilmManager.Models.Rentals;
using FilmManager.ViewMovie;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using INavigationService = FilmManager.Core.Services.Navigate.INavigationService;

namespace FilmManager.Features.Rentals.UpdateRentals;

public partial class ViewUpdateRentalViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    private readonly IRentalsService _rentalsService = App.Current.Services.GetService<IRentalsService>()!;
    [ObservableProperty] private bool _isTextBoxEnabled;
    [ObservableProperty] private RentalsModel? _rentals;

    public ViewUpdateRentalViewModel()
    {
        _navigationService = App.Current.Services.GetService<INavigationService>()!;
        Rentals = _rentalsService.GetRentalsSelected();
        _isTextBoxEnabled = false;
    }

    [RelayCommand]
    private void OnToggleEdit()
    {
        IsTextBoxEnabled = true;
    }


    [RelayCommand]
    private void OnGuardarRental()
    {
        var rentalSeleccionado = Rentals;

        if (rentalSeleccionado != null)
        {
            var result = MessageBox.Show(
                $"¿Está seguro que desea modificar la renta de id: {rentalSeleccionado.RentalId}?",
                "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {

                _rentalsService.UpdateRental(rentalSeleccionado);
                _navigationService.Navigate(typeof(ViewMoviePage));
            }
        }
        else
        {
            MessageBox.Show("No se encontro la renta.");
        }
    }


    [RelayCommand]
    public void OnDeleteRental()
    {
        var rentalSeleccionado = _rentalsService.GetRentalsSelected();

        if (rentalSeleccionado != null)
        {
            var result = MessageBox.Show(
                $"¿Está seguro que desea eliminar la renta con id: {rentalSeleccionado.RentalId}?",
                "Confirmación", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.OK)
            {
                _rentalsService.DeleteRental(rentalSeleccionado.RentalId);
                _navigationService.Navigate(typeof(ViewRentalsPage));
            }
        }
    }
}