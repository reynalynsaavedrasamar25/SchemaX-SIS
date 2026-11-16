using Microsoft.Extensions.Logging;
using SchemaX.Mobile.Services;
using SchemaX.Mobile.Pages;

namespace SchemaX.Mobile
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<IStudentService, FakeStudentService>();
            builder.Services.AddTransient<DirectoryPage>();
            builder.Services.AddTransient<StudentFormPage>();
            return builder.Build();
        }
    }
}
