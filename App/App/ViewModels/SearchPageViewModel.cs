using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels
{
    public partial class SearchPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public SearchPageViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [ObservableProperty]
        private string _searchTerm;

        [ObservableProperty]
        private List<User> _users;

        [RelayCommand]
        public async Task Search()
        {
            Users = await _userService.Search(App.User.Id, SearchTerm);
        }

        public void OnAppearing()
        {
            SearchTerm = string.Empty;
            Users = new List<User>();
        }
    }
}
