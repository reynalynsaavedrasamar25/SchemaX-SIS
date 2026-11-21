using Microsoft.Extensions.Logging;
using SchemaX.Mobile.Services;
using SchemaX.Mobile.ViewModels;
using SchemaX.Mobile.Views;

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

            // Services
            builder.Services.AddSingleton<IStudentService, FakeStudentService>(); // REPLACE WITH REAL SERVICE IMPLEMENTATION

            // Login
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            // Student list
            builder.Services.AddTransient<StudentListViewModel>();
            builder.Services.AddTransient<StudentListPage>();

            // Student detail
            builder.Services.AddTransient<StudentDetailViewModel>();
            builder.Services.AddTransient<StudentDetailPage>();

            return builder.Build();
        }
    }
}