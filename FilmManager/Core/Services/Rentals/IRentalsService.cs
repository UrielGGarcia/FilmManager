using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Models.Rentals;

namespace FilmManager.Core.Services.Rentals;

public interface IRentalsService
{
    public Task<Response<ObservableCollection<RentalsModel>>> GetListaRentals(int page);
}