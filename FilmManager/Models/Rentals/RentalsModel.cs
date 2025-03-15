using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Rentals;

[JsonObject(MemberSerialization.OptIn)]
public partial class RentalsModel : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "RentalId")]
    private int _rentalId;
    
    [ObservableProperty] [JsonProperty(PropertyName = "CustomerId")]
    private int _customerId;

    [ObservableProperty] [JsonProperty(PropertyName = "InventoryId")]
    private int _inventoryId;

    [ObservableProperty] [JsonProperty(PropertyName = "LastUpdate")]
    private DateTime _lastUpdate;

    [ObservableProperty] [JsonProperty(PropertyName = "RentalDate")]
    private DateTime _rentalDate;

    [ObservableProperty] [JsonProperty(PropertyName = "ReturnDate")]
    private DateTime? _returnDate;

    [ObservableProperty] [JsonProperty(PropertyName = "StaffId")]
    private int _staffId;

    [ObservableProperty] [JsonProperty(PropertyName = "SpecialFeatures")]
    private List<string>? _specialFeatures;
}