using App.Pages;
using App.Repositories;
using App.Services;
using App.ViewModels;
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
        builder.Services.AddSingleton<IUserService, LocalUserService>();
		builder.Services.AddSingleton<IPostService, LocalPostService>();

		builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<RegisterPage>();

        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<ProfilePage>();

        builder.Services.AddSingleton<AddPageViewModel>();
        builder.Services.AddSingleton<AddPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
