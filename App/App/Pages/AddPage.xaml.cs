using App.ViewModels;

namespace App.Pages;

public partial class AddPage : ContentPage
{
	private readonly AddPageViewModel _addPageViewModel;


    public AddPage(AddPageViewModel addPageViewModel)
	{
		InitializeComponent();
		BindingContext = addPageViewModel;
		_addPageViewModel = addPageViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_addPageViewModel.OnAppearing();
    }
}