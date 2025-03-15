using CommunityToolkit.Mvvm.ComponentModel;
using FilmManager.Core.Services.Rentals;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;

namespace FilmManager.Features.Rentals.UpdateRentals;

public partial class ViewUpdateRentalViewModel : ObservableObject
{
    private readonly IRentalsService _rentalsService = App.Current.Services.GetService<IRentalsService>()!;
    private readonly INavigationService _navigationService = App.Current.Services.GetService<INavigationService>()!;
    [ObservableProperty] private RentalsModel? _rentals;
    [ObservableProperty] private bool _isTextBoxEnabled;

    public ViewUpdateRentalViewModel()
    {
        Rentals = _rentalsService.GetRentalsSelected();
        _isTextBoxEnabled = false;
    }
    
}