using App.ViewModels;

namespace App.Pages;

public partial class FeedPage : ContentPage
{
    private readonly FeedPageViewModel _feedPageViewModel;

    public FeedPage(FeedPageViewModel feedPageViewModel)
    {
        InitializeComponent();
        BindingContext = feedPageViewModel;
        _feedPageViewModel = feedPageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _feedPageViewModel.OnAppearing();
    }
}