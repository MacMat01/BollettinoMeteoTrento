#region

using System.Text.Json;
using bollettino_meteo_trento.web.Models;

#endregion
namespace bollettino_meteo_trento.web.Services;

public sealed class MeteoService(HttpClient httpClient)
{

    internal async Task<RootObject?> DaiMeteoDataAsync(string url)
    {
        string risposta = await httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<RootObject>(risposta);
    }
}
