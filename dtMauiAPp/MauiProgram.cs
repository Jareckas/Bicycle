﻿using Microsoft.Extensions.Logging;
using dtMauiAPp.ViewModels;
using dtMauiAPp.Services;
using dtMauiAPp.Views;
using CommunityToolkit;

namespace dtMauiAPp
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
            builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
            { 
#if ANDROID
                return new AndroidHttp();
#else
                return null!;
#endif


            });
            builder.Services.AddHttpClient("custom-httpclient", httpClient =>
            {
                var baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7154" : "https://10.0.2.2:7154";
                httpClient.BaseAddress = new Uri(baseAddress);
            }).ConfigureHttpMessageHandlerBuilder(configBuilder =>
            {
                var platformMessageHandler = configBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                configBuilder.PrimaryHandler = platformMessageHandler.GetHttpMessageHandler();
            });
            builder.Services.AddSingleton<AppShell>();

            builder.Services.AddSingleton<ClientService>();
            builder.Services.AddSingleton<AuthenticationPage>();
            builder.Services.AddSingleton<AuthenticationPageViewModel>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<WeatherForecastPage>();
            builder.Services.AddSingleton<WeatherForecastViewModel>();
            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
