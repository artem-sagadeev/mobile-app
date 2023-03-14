using App.Models;
using App.Pages;
using App.Repositories;
using App.Services;
using App.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App;

public partial class App : Application
{
    public static User User { get; set; }

	public App()
	{
		InitializeComponent();

        MainPage = new AppShell();
    }
}
