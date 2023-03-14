using App.ViewModels;

namespace App.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly RegisterPageViewModel _registerPageViewModel;


    public RegisterPage(RegisterPageViewModel registerPageViewModel)
	{
		InitializeComponent();
		BindingContext = registerPageViewModel;
        _registerPageViewModel = registerPageViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _registerPageViewModel.OnAppearing();
    }
}