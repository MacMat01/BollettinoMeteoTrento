#region

using BollettinoMeteoTrento.Services;
using Microsoft.Extensions.Logging;
using MeteoViewModel = BollettinoMeteoTrento.MAUI.ViewModels.MeteoViewModel;

#endregion
namespace BollettinoMeteoTrento.MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MeteoService>();
        builder.Services.AddTransient<MeteoViewModel>();
        builder.Services.AddTransient<MeteoPage>();

        return builder.Build();
    }
}
