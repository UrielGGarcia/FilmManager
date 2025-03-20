using System.Collections.ObjectModel;
using FilmManager.Models.Customer;

namespace FilmManager.Core.Services.CustomerService;

public interface ICustomerService
{
    Task<ObservableCollection<CustomerModel>> GetListCustomers();
}