#region

using bollettino_meteo_trento.web.Models;
using bollettino_meteo_trento.web.Services;
using Microsoft.AspNetCore.Mvc;

#endregion
namespace bollettino_meteo_trento.web.Controllers;

public sealed class MeteoController(MeteoService weatherService) : Controller
{

    public async Task<IActionResult> Index()
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await weatherService.DaiMeteoDataAsync(url);

        return View(meteoData);
    }
}
