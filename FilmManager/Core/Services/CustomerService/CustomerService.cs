using System.Collections.ObjectModel;
using FilmManager.Core.Services.CustomerService.Network;
using FilmManager.Core.Services.Films;
using FilmManager.Models.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace FilmManager.Core.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private ObservableCollection<CustomerModel> _customers = new();
    private IFilmService _filmServiceImplementation;
    private CustomerServiceNetwork? _filmServiceNetwork = App.Current.Services.GetService<CustomerServiceNetwork>();

    public async Task<ObservableCollection<CustomerModel>> GetListCustomers()
    {
        _customers = await CustomerServiceNetwork.GetCustomers();
        return _customers;
    }
}