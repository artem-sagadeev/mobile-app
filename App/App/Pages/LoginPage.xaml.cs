using App.ViewModels;

namespace App.Pages;

public partial class LoginPage : ContentPage
{
	private readonly LoginPageViewModel _loginPageViewModel;

    public LoginPage(LoginPageViewModel loginPageViewModel)
	{
		InitializeComponent();
		BindingContext = loginPageViewModel;
        _loginPageViewModel = loginPageViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _loginPageViewModel.OnAppearing();
    }
}