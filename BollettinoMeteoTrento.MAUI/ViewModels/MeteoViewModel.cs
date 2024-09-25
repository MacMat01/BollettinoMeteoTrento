#region

using System.Collections.ObjectModel;
using System.Windows.Input;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

#endregion
namespace BollettinoMeteoTrento.MAUI.ViewModels;

public sealed partial class MeteoViewModel : ObservableObject
{
    private readonly MeteoService _meteoService;

    [ObservableProperty]
    private ObservableCollection<Previsione> _previsioni = new ObservableCollection<Previsione>();

    public MeteoViewModel()
    {
        _meteoService = new MeteoService(new HttpClient());
        CaricaPrevisioniCommand = new AsyncRelayCommand(async () => await CaricaPrevisioni());
    }

    public MeteoViewModel(MeteoService meteoService)
    {
        _meteoService = meteoService;
        CaricaPrevisioniCommand = new AsyncRelayCommand(async () => await CaricaPrevisioni());
    }

    public ICommand CaricaPrevisioniCommand { get; }

    private async Task CaricaPrevisioni()
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await _meteoService.DaiMeteoDataAsync(url);

        if (meteoData?.previsione != null)
        {
            Previsioni.Clear();
            foreach (Previsione previsione in meteoData.previsione)
            {
                Previsioni.Add(previsione);
            }
        }
    }
}
