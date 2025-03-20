using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Rentals;

[JsonObject(MemberSerialization.OptIn)]
public partial class RentalsModel : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "customerId")]
    private int _customerId;

    [ObservableProperty] [JsonProperty(PropertyName = "inventoryId")]
    private int _inventoryId;

    [ObservableProperty] [JsonProperty(PropertyName = "lastUpdate")]
    private DateTime _lastUpdate;

    [ObservableProperty] [JsonProperty(PropertyName = "rentalDate")]
    private DateTime? _rentalDate;

    [ObservableProperty] [JsonProperty(PropertyName = "rentalId")]
    private int _rentalId;

    [ObservableProperty] [JsonProperty(PropertyName = "returnDate")]
    private DateTime? _returnDate;

    [ObservableProperty] [JsonProperty(PropertyName = "staffId")]
    private int _staffId;
}