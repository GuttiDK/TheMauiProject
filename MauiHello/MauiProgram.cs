using Microsoft.Extensions.Logging;
using MauiHello.Services;
using MauiHello.ViewModels;
using MauiHello.Views;

namespace MauiHello
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton<MonkeyService>();

            // Register ViewModels
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<MonkeyDetailsPageViewModel>();

            // Register Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<MonkeyDetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
