using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Core.Services.Rentals.Network;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Core.Services.Rentals;

public class RentalsService : IRentalsService
{
    //Declaración de lista vacia 
    private ObservableCollection<RentalsModel> _rentals = new();
    private RentalsServiceNetwork? _rentalsServiceNetwork = App.Current.Services.GetService<RentalsServiceNetwork>();
    private bool _rentalsTF;
    
    private RentalsModel? _selectedRental;

    public async Task<Response<ObservableCollection<RentalsModel>>> GetListaRentals(int page)
    {
        var response = await RentalsServiceNetwork.GetRentalsAsync(page, 1000);
        return response;
    }
    

    public async Task<ObservableCollection<RentalsModel>> UpdateRentals(RentalsModel rentals)
    {
        var index =_rentals.IndexOf(rentals);   
        if (index != 0) _rentals[index] = rentals;

        _rentalsTF = await RentalsServiceNetwork.UpdateRentals(rentals);
        return _rentals;
    }

    public void SetRentalsSelected(RentalsModel rentals)
    {
        _selectedRental = rentals;
    }

    public RentalsModel? GetRentalsSelected()
    {
        return _selectedRental;
    }
}