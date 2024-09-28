#region

using System.Net.Http.Json;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services.StorageServices;
using BollettinoMeteoTrento.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

#endregion

namespace BollettinoMeteoTrento.MAUI.ViewModels.Authentication;

public sealed partial class LoginViewModel : ObservableObject
{
    private readonly HttpClient _httpClient;
    private readonly JwtStorageService _jwtStorageService;

    public LoginViewModel() : this(new JwtStorageService())
    {
    }

    public LoginViewModel(JwtStorageService jwtStorageService)
    {
        _jwtStorageService = jwtStorageService;
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5145/")
        };
        User = new User();
    }

    [field: ObservableProperty]
    public User User
    {
        get;
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.HashedPassword))
            {
                await Shell.Current.DisplayAlert("Errore", "Per favore inserisci email e password.", "OK");
                return;
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/Login", User);
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Errore", "Login fallito. Motivo: " + $"{response.StatusCode}", "OK");
                return;
            }

            JwtResponse? result = await response.Content.ReadFromJsonAsync<JwtResponse>();

            if (result != null)
            {
                await _jwtStorageService.StoreTokenAsync(result.Token);
                await Shell.Current.GoToAsync("//MeteoPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Errore", "Il token JWT non è stato restituito.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Errore", $"Si è verificato un errore durante il login: {ex.Message}", "OK");
        }
    }
}
