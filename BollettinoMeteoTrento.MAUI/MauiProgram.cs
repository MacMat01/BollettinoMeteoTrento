#region

using BollettinoMeteoTrento.MAUI.Pages.Meteo;
using BollettinoMeteoTrento.MAUI.ViewModels.Authentication;
using BollettinoMeteoTrento.MAUI.ViewModels.Meteo;
using BollettinoMeteoTrento.Services.StorageServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.Extensions.Logging;

#endregion

namespace BollettinoMeteoTrento.MAUI;

static class MauiProgram
{
    internal static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(static fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<JwtStorageService>();
        builder.Services.AddSingleton<IJwtUtils, JwtUtils>();

        builder.Services.AddTransient<MeteoViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MeteoPage>();

        return builder.Build();
    }
}
