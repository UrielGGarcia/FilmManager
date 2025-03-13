using System.Net.Http;
using System.Net.Http.Headers;

namespace FilmManager.Core.Services;

public class FilmClientHandler
{
    public static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("http://localhost:5084/"),

        DefaultRequestHeaders =
        {
            Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
        }
    };
}