using Microsoft.Extensions.Logging;
using MauiApp2.Data;
using MudBlazor.Services;
using Microsoft.Maui.Controls;
using MudBlazor;
using MauiApp2.Services.SaleService;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Builder;
using CommunityToolkit.Maui;

namespace MauiApp2;

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
			})
			.UseMauiCommunityToolkit();

        builder.Services.AddMudServices(configuration =>
                {
            configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
            configuration.SnackbarConfiguration.HideTransitionDuration = 100;
            configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
            configuration.SnackbarConfiguration.VisibleStateDuration = 6000;
            configuration.SnackbarConfiguration.ShowCloseIcon = true;
        });
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"PersonDB.db3");

		builder.Services.AddSingleton<ISaleRepository,SaleService>(p=> ActivatorUtilities.CreateInstance<SaleService>(p,dbPath));

		return builder.Build();
	}
}
