using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using GuiguemdeApp.Carillon.Service;
using GuiguemdeApp.Carillon.View;
using GuiguemdeApp.Carillon.ViewModel;

namespace GuiguemdeApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<AlarmService>();
        builder.Services.AddSingleton<AlarmScheduler>();

        builder.Services.AddTransient<SirenViewModel>();
        builder.Services.AddTransient<SirenView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}