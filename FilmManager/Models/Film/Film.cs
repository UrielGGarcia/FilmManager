namespace FilmManager.Models.Film;

public class Film
{
    public ushort FilmId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int ReleaseYear { get; set; }
    public byte LanguageId { get; set; }
    public byte? OriginalLanguageId { get; set; }
    public byte RentalDuration { get; set; }
    public decimal RentalRate { get; set; }
    public ushort? Length { get; set; }
    public decimal ReplacementCost { get; set; }
    public Rating Rating { get; set; }
    public List<string>? SpecialFeatures { get; set; }
    public DateTime LastUpdate { get; set; }
    public byte? CategoryId { get; set; }
}

public enum Rating
{
    G,
    PG,
    PG_13,
    R,
    NC_17 // NC-17 en C#
}