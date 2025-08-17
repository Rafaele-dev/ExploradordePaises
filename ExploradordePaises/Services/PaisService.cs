using ExploradordePaises.Models;
using System.Net.Http.Json;

namespace ExploradordePaises.Services;

public class PaisService
{
    private readonly HttpClient _httpClient;

    public PaisService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://restcountries.com/v3.1/")
        };
    }

    public async Task<List<Pais>> GetPaisesAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<Pais>>(
            "all?fields=name,capital,region,flags,currencies");

        return result ?? new List<Pais>();
    }
}
