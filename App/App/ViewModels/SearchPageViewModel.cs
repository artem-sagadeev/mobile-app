using App.Dtos;
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
        private List<SearchUserDto> _users;

        [RelayCommand]
        public async Task Search()
        {
            var users = await _userService.Search(App.User.Id, (string)this.SearchTerm);
            var subscribtionsIds = await _userService.GetSubscriptionsIds(App.User.Id);

            Users = users
                .Select(user => new SearchUserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    IsSubscribedTo = subscribtionsIds.Contains(user.Id),
                    IsNotSubscribedTo = !subscribtionsIds.Contains(user.Id)
                })
                .ToList();
        }

        [RelayCommand]
        public async Task Subscribe(SearchUserDto dto)
        {
            await _userService.Subscribe(dto.Id);
            await Search();
        }

        [RelayCommand]
        public async Task Unsubscribe(SearchUserDto dto)
        {
            await _userService.Unsubscribe(dto.Id);
            await Search();
        }

        public void OnAppearing()
        {
            SearchTerm = string.Empty;
            Users = new List<SearchUserDto>();
        }
    }
}
