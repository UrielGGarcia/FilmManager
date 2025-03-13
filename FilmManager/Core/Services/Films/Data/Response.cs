namespace FilmManager.Core.Services.Films.Data;

public class Response<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public T Data { get; set; }
}