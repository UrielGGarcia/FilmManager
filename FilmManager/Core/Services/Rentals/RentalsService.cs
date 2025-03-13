using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Core.Services.Rentals.Network;
using FilmManager.Models.Rentals;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Core.Services.Rentals;

public class RentalsService : IRentalsService
{
    private ObservableCollection<RentalsModel> _rentals = new();
    private RentalsServiceNetwork? _rentalsServiceNetwork = App.Current.Services.GetService<RentalsServiceNetwork>();

    public async Task<Response<ObservableCollection<RentalsModel>>> GetListaRentals(int page)
    {
        var response = await RentalsServiceNetwork.GetRentalsAsync(page, 1000);
        return response;
    }
}