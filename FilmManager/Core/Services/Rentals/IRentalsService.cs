using System.Collections.ObjectModel;
using FilmManager.Core.Services.Films.Data;
using FilmManager.Models.Rentals;

namespace FilmManager.Core.Services.Rentals;

public interface IRentalsService
{
    Task<Response<ObservableCollection<RentalsModel>>> GetListaRentals(int page);
    
    RentalsModel? GetRentalsSelected();
    
    void SetRentalsSelected(RentalsModel rentals);
    
    Task <ObservableCollection<RentalsModel>> UpdateRentals(RentalsModel rentals);
}