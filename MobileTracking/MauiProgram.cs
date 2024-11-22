using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MobileTracking.Pages;
using MobileTracking.ViewModels;
using Refit;
using Shared.Mobile;
using Shared.Mobile.Delegates;
using Shared.Mobile.Services;
using CommunityToolkit.Maui;

namespace MobileTracking;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterAppViewModels()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddScoped<HeaderTokenHandler>();
        var apiData = Constants.Api.BASE_URL;
        mauiAppBuilder.Services.AddRefitClient<IAuthService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData));
        //mauiAppBuilder.Services.AddRefitClient<IOrderService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData)).AddHttpMessageHandler<HeaderTokenHandler>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingletonWithShellRoute<LoginPage, LoginViewModel>(nameof(LoginPage));
        return mauiAppBuilder;
    }
}