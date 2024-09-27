#region

using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services.StorageServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

#endregion
namespace BollettinoMeteoTrento.MAUI.ViewModels;

public sealed partial class MeteoViewModel : ObservableObject
{
    private const string MeteoApiUrl = "http://localhost:5145/api/Meteo";
    private readonly HttpClient _httpClient;
    private readonly JwtStorageService _jwtStorageService;

    [ObservableProperty]
    private ObservableCollection<Giorni> _previsioni = new ObservableCollection<Giorni>();

    [ObservableProperty]
    private string? _selectedDate;

    public MeteoViewModel() : this(new JwtStorageService())
    {
    }

    private MeteoViewModel(JwtStorageService jwtStorageService)
    {
        _jwtStorageService = jwtStorageService;
        _httpClient = new HttpClient();
        CercaPrevisioniCommand = new AsyncRelayCommand(CercaPrevisioni);
    }

    public ICommand CercaPrevisioniCommand { get; set; }

    private async Task CercaPrevisioni()
    {
        try
        {
            await LoadMeteoData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
        }
    }

    private async Task LoadMeteoData()
    {
        string? jwtToken = await _jwtStorageService.RetrieveTokenAsync();

        if (!string.IsNullOrWhiteSpace(jwtToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }

        HttpResponseMessage response = await _httpClient.GetAsync(MeteoApiUrl);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error: API call failed with status code {response.StatusCode}");
            return;
        }

        RootObject? meteoData = await response.Content.ReadFromJsonAsync<RootObject>();

        if (meteoData?.previsione != null)
        {
            Previsioni.Clear();
            FilterGiorni(meteoData);
        }
    }

    private void FilterGiorni(RootObject meteoData)
    {
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
