using App.ViewModels;

namespace App.Pages;

public partial class SearchPage : ContentPage
{
	private readonly SearchPageViewModel _searchPageViewModel;

	public SearchPage(SearchPageViewModel searchPageViewModel)
	{
		InitializeComponent();
		BindingContext = searchPageViewModel;
		_searchPageViewModel = searchPageViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _searchPageViewModel.OnAppearing();
    }
}