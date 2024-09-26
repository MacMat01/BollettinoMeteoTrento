#region

using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services;
using Microsoft.AspNetCore.Mvc;

#endregion
namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MeteoController(MeteoService meteoService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await meteoService.DaiMeteoDataAsync(url);

        if (meteoData is null)
        {
            return NotFound();
        }

        return Ok(meteoData);
    }
}
