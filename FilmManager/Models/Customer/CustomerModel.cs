using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Customer;

[JsonObject(MemberSerialization.OptIn)]
public partial class CustomerModel : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "customerId")]
    private int _customerId;

    [ObservableProperty] [JsonProperty(PropertyName = "email")]
    private string? _email;

    [ObservableProperty] [JsonProperty(PropertyName = "firstName")]
    private string? _firtsName;

    [ObservableProperty] [JsonProperty(PropertyName = "lastName")]
    private string? _lastName;

    public string FullName => $"{FirtsName} {LastName}";
}