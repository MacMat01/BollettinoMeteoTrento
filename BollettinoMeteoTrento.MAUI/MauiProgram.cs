#region

using BollettinoMeteoTrento.Services.StorageServices;
using Microsoft.Extensions.Logging;
using MeteoViewModel = BollettinoMeteoTrento.MAUI.ViewModels.MeteoViewModel;

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
        builder.Services.AddTransient<MeteoViewModel>();
        builder.Services.AddTransient<MeteoPage>();

        return builder.Build();
    }
}
