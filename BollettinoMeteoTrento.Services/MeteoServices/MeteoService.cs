#region

using System.Text.Json;
using BollettinoMeteoTrento.Domain;

#endregion
namespace BollettinoMeteoTrento.Services;

public sealed class MeteoService(HttpClient httpClient)
{

    public async Task<RootObject?> DaiMeteoDataAsync(string url)
    {
        string risposta = await httpClient.GetStringAsync(url);
        return JsonSerializer.Deserialize<RootObject>(risposta);
    }
}
