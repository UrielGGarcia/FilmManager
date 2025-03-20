using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Core.Services.Rentals.Network;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FilmManager.Core.Services.Rentals;

public class RentalsService : IRentalsService
{
    //Declaración de lista vacia 
    private readonly ObservableCollection<RentalsModel> _rentals = new();
    private RentalsServiceNetwork? _rentalsServiceNetwork = App.Current.Services.GetService<RentalsServiceNetwork>();
    private bool _rentalsTR;

    private RentalsModel? _selectedRental;

    public async Task<Response<ObservableCollection<RentalsModel>>> GetListaRentals(int page)
    {
        var response = await RentalsServiceNetwork.GetRentalsAsync(page, 1000);
        return response;
    }


    public async Task<ObservableCollection<RentalsModel>> UpdateRental(RentalsModel rental)
    {
        Console.WriteLine("Entró aquí en el service II");
        var index = _rentals.IndexOf(rental);
        if (index >= 0) _rentals[index] = rental;


        var Json = JsonConvert.SerializeObject(rental);

        Console.WriteLine($"Cuerpo  que recibe el Service II {Json}");

        _rentalsTR = await RentalsServiceNetwork.UpdateRental(rental);
        return _rentals;
    }

    public async Task<ObservableCollection<RentalsModel>> DeleteRental(int rentalId)
    {
        _rentalsTR = await RentalsServiceNetwork.DeleteRental(rentalId);
        return _rentals;
    }


    public async Task<bool> AddRental(RentalsModel rental)
    {
        _rentalsTR = await RentalsServiceNetwork.AddRental(rental);
        return _rentalsTR;
    }


    public void SetRentalsSelected(RentalsModel rental)
    {
        _selectedRental = rental;
    }


    public RentalsModel? GetRentalsSelected()
    {
        return _selectedRental;
    }
}