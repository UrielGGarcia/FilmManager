using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Film;

[JsonObject(MemberSerialization.OptIn)]
public partial class AddFilmResource : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "Description")]
    private string? _description;

    [ObservableProperty] [JsonProperty(PropertyName = "LanguageId")]
    private byte _languageId;

    [ObservableProperty] [JsonProperty(PropertyName = "Length")]
    private ushort? _length;

    [ObservableProperty] [JsonProperty(PropertyName = "Rating")]
    private string? _rating;

    [ObservableProperty] [JsonProperty(PropertyName = "ReleaseYear")]
    private int _releaseYear;

    [ObservableProperty] [JsonProperty(PropertyName = "RentalDuration")]
    private byte _rentalDuration;

    [ObservableProperty] [JsonProperty(PropertyName = "RentalRate")]
    private decimal _rentalRate;

    [ObservableProperty] [JsonProperty(PropertyName = "ReplacementCost")]
    private decimal _replacementCost;

    [ObservableProperty] [JsonProperty(PropertyName = "SpecialFeatures")]
    private List<string>? _specialFeatures;

    [ObservableProperty] [JsonProperty(PropertyName = "Title")]
    private string? _title;
}