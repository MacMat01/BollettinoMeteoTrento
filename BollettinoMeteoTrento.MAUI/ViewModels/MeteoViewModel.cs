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
    private ObservableCollection<Giorni> _previsioni = new ObservableCollection<Giorni>();

    [ObservableProperty]
    private string? _selectedDate;

    public MeteoViewModel()
    {
        _meteoService = new MeteoService(new HttpClient());
        CercaPrevisioniCommand = new AsyncRelayCommand(CercaPrevisioni);
    }

    public MeteoViewModel(MeteoService meteoService)
    {
        _meteoService = meteoService;
        CercaPrevisioniCommand = new AsyncRelayCommand(CercaPrevisioni);
    }

    public ICommand CercaPrevisioniCommand { get; set; }

    private async Task CercaPrevisioni()
    {
        const string url = "https://www.meteotrentino.it/protcivtn-meteo/api/front/previsioneOpenDataLocalita?localita=TRENTO";
        RootObject? meteoData = await _meteoService.DaiMeteoDataAsync(url);

        if (meteoData?.previsione != null)
        {
            Previsioni.Clear();
            if (string.IsNullOrEmpty(SelectedDate))
            {
                foreach (Giorni giorno in meteoData.previsione.SelectMany(static previsione => previsione.giorni))
                {
                    Previsioni.Add(giorno);
                }
            }
            else
            {
                foreach (Giorni giorno in meteoData.previsione.SelectMany(previsione => previsione.giorni.Where(g => g.giorno == SelectedDate)))
                {
                    Previsioni.Add(giorno);
                }
            }
        }
    }
}
