using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Inventory;

[JsonObject(MemberSerialization.OptIn)]
public partial class InventoryModel : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "filmId")]
    private int _filmId;

    [ObservableProperty] [JsonProperty(PropertyName = "inventoryId")]
    private int _inventoryId;

    [ObservableProperty] [JsonProperty(PropertyName = "storeId")]
    private int _storeId;

    public string CopiasFilm => $"Copia en tienda {StoreId}";
}