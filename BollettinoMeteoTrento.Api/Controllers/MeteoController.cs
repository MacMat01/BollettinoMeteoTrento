#region

using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services;
using Microsoft.AspNetCore.Mvc;

#endregion
namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class MeteoController(MeteoService weatherService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await weatherService.DaiMeteoDataAsync(url);

        if (meteoData is null)
        {
            return NotFound();
        }

        return Ok(meteoData);
    }
}
