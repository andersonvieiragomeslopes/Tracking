using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MobileTracking.Pages;
using MobileTracking.ViewModels;
using Refit;
using Shared.Mobile;
using Shared.Mobile.Delegates;
using Shared.Mobile.Services.Requests;
using Shared.Mobile.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SimpleToolkit.Core;
using SimpleToolkit.SimpleShell;
using MobileTracking.Services;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using Shared.Mobile.Repositories;
using Controls.UserDialogs.Maui;



#if ANDROID || IOS
using Maui.GoogleMaps.Clustering.Hosting;
using Maui.GoogleMaps.Hosting;
#endif

namespace MobileTracking;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterAppViewModels()
            .RegisterServiceLocator()
            .UseUserDialogs()
            .UseSkiaSharp()
            .UseSimpleToolkit()
            .UseSimpleShell()
            .ConfigureMopups()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
#if ANDROID
        builder.UseGoogleMaps();
#elif IOS
        builder.UseGoogleMaps("");
#endif
#if ANDROID || IOS
        builder.UseGoogleMapsClustering();
#endif
        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {        
        mauiAppBuilder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        mauiAppBuilder.Services.AddSingleton(Geolocation.Default);
        mauiAppBuilder.Services.AddSingleton<IUserDialogs>(s => UserDialogs.Instance);

        mauiAppBuilder.Services.AddScoped<HeaderTokenHandler>();
        var apiData = Constants.Api.BASE_URL;

        mauiAppBuilder.Services.AddSingleton<IApiRequestService, ApiRequestService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        mauiAppBuilder.Services.AddSingleton<ILocationService, LocationService>();
        mauiAppBuilder.Services.AddSingleton<IActivityIndicatorService, ActivityIndicatorService>();
        mauiAppBuilder.Services.AddSingleton<IInitalizeBackgroundService, InitalizeBackgroundService>();


        mauiAppBuilder.Services.AddSingleton<IOrdersRepository, OrdersRepository>();
        mauiAppBuilder.Services.AddSingleton<ICacheService, CacheService>();


        #region Refit
        mauiAppBuilder.Services.AddRefitClient<IAuthService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData)); 
        mauiAppBuilder.Services.AddRefitClient<IOrderService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData)).AddHttpMessageHandler<HeaderTokenHandler>();
        mauiAppBuilder.Services.AddRefitClient<IDrivingService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData)).AddHttpMessageHandler<HeaderTokenHandler>();
        #endregion


        
        //mauiAppBuilder.Services.AddRefitClient<IOrderService>().ConfigureHttpClient(c => c.BaseAddress = new Uri(apiData)).AddHttpMessageHandler<HeaderTokenHandler>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterServiceLocator(this MauiAppBuilder mauiAppBuilder)
    {
        #region Service Locator
        ServiceLocator.Configure(mauiAppBuilder.Services);
        ServiceLocator.Instance.Init();
        #endregion
        return mauiAppBuilder;
    }
    public static MauiAppBuilder RegisterAppViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransientWithShellRoute<LoginPage, LoginViewModel>(nameof(LoginPage));
        mauiAppBuilder.Services.AddSingletonWithShellRoute<LoadingPage, LoadingViewModel>(nameof(LoadingPage));
        mauiAppBuilder.Services.AddSingletonWithShellRoute<HomePage, HomeViewModel>(nameof(HomePage));
 
        return mauiAppBuilder;
    }
}