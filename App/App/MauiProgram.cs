using App.Pages;
using App.Repositories;
using App.Services;
using App.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App;

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

		builder.Services.AddSingleton<UsersRepository>();
        builder.Services.AddSingleton<PostsRepository>();
        builder.Services.AddSingleton<IUserService, RemoteUserService>();
		builder.Services.AddSingleton<IPostService, RemotePostService>();

		builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<RegisterPage>();

        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<ProfilePage>();

        builder.Services.AddSingleton<AddPageViewModel>();
        builder.Services.AddSingleton<AddPage>();

		builder.Services.AddSingleton<FeedPageViewModel>();
		builder.Services.AddSingleton<FeedPage>();

		builder.Services.AddSingleton<SearchPageViewModel>();
		builder.Services.AddSingleton<SearchPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
