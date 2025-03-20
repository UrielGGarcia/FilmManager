using System.Collections.ObjectModel;
using System.Net.Http;
using FilmManager.Models.Customer;
using Newtonsoft.Json;

namespace FilmManager.Core.Services.CustomerService.Network;

public class CustomerServiceNetwork
{
    private static readonly HttpClient _client = FilmClientHandler.Client;

    // GET
    public static async Task<ObservableCollection<CustomerModel>> GetCustomers()
    {
        var response = await _client.GetAsync("api/customer");
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ObservableCollection<CustomerModel>>(responseContent) ??
               new ObservableCollection<CustomerModel>();
    }
}