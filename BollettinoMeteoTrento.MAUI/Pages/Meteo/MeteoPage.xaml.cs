#region

using BollettinoMeteoTrento.MAUI.Pages.Authentication;

#endregion
namespace BollettinoMeteoTrento.MAUI.Pages.Meteo;

public sealed partial class MeteoPage : ContentPage
{
    public MeteoPage()
    {
        InitializeComponent();
    }

    private async void OnLoginPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }

    private async void OnRegisterPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
