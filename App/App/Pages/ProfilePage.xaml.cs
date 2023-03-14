using App.ViewModels;

namespace App.Pages;

public partial class ProfilePage : ContentPage
{
    private ProfilePageViewModel _profilePageViewModel;

    public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		InitializeComponent();
		BindingContext = profilePageViewModel;
        _profilePageViewModel = profilePageViewModel;   
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _profilePageViewModel.OnAppearing();
    }
}