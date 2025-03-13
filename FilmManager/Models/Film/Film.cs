using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace FilmManager.Models.Film;

[JsonObject(MemberSerialization.OptIn)]
public partial class Film : ObservableObject
{
    [ObservableProperty] [JsonProperty(PropertyName = "Title")]
    private string? _title;
    
    [ObservableProperty] [JsonProperty(PropertyName = "Description")]
    private string? _description;

    [ObservableProperty] [JsonProperty(PropertyName = "FilmId")]
    private ushort _filmId;

    [ObservableProperty] [JsonProperty(PropertyName = "LanguageId")]
    private byte _languageId;

    [ObservableProperty] [JsonProperty(PropertyName = "LastUpdate")]
    private DateTime _lastUpdate;

    [ObservableProperty] [JsonProperty(PropertyName = "Length")]
    private ushort? _length;

    [ObservableProperty] [JsonProperty(PropertyName = "OriginalLanguageId")]
    private byte? _originalLanguageId;

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

    
}