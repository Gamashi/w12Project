using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using w12.Services;
using w12.ViewModels;
using w12.Views;

namespace w12
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<Database>();
            builder.Services.AddTransient<AddNewBaseExerciseViewModel>();
            builder.Services.AddTransient<AddNewBaseExercise>();
            return builder.Build();
        }
    }
}
