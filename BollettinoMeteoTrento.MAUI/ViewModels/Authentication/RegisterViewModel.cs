﻿#region

using System.Net.Http.Json;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services.StorageServices;
using BollettinoMeteoTrento.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

#endregion

namespace BollettinoMeteoTrento.MAUI.ViewModels.Authentication;

public sealed partial class RegisterViewModel(JwtStorageService jwtStorageService) : ObservableObject
{
    private readonly HttpClient _httpClient = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5145/")
    };

    public RegisterViewModel() : this(new JwtStorageService())
    {
    }

    [field: ObservableProperty]
    public User User
    {
        get;
    } = new User();

    [RelayCommand]
    private async Task RegisterAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(User.Email) || string.IsNullOrWhiteSpace(User.Password))
            {
                await Shell.Current.DisplayAlert("Errore", "Per favore inserisci email e password.", "OK");
                return;
            }

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/Register", User);
            if (!response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Errore", "Registrazione fallita. Motivo: " + $"{response.StatusCode} ", "OK");
                return;
            }

            JwtResponse? result = await response.Content.ReadFromJsonAsync<JwtResponse>();

            if (result != null)
            {
                await jwtStorageService.StoreTokenAsync(result.Token);
                await Shell.Current.GoToAsync("//MeteoPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Errore", "Il token JWT non è stato restituito.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Errore", $"Si è verificato un errore durante la registrazione: {ex.Message}", "OK");
        }
    }
}
