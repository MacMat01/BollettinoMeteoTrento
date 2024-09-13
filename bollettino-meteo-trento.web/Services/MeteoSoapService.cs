#region

using System.Diagnostics;
using bollettino_meteo_trento.web.Models;
using CoreWCF;

#endregion
namespace bollettino_meteo_trento.web.Services;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
public class MeteoSoapService(MeteoService meteoService) : IMeteoSoapService
{

    public async Task<RootObject> DaiMeteoDaGiornoAsync(string giorno)
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await meteoService.DaiMeteoDataAsync(url);

        // Filtra i dati per il giorno specificato
        Debug.Assert(meteoData != null, nameof(meteoData) + " != null");
        RootObject risultato = meteoData with
        {
            previsione = meteoData.previsione.Select(p => p with
            {
                giorni = p.giorni.Where(g => g.giorno == giorno).ToArray()
            }).ToArray()
        };

        return risultato;
    }
}
