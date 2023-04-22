using App.Models;
using App.Pages;
using App.Repositories;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace App.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly LoggedInUsersRepository _loggedInUsersRepository;

        public LoginPageViewModel(IUserService loginService, LoggedInUsersRepository loggedInUsersRepository)
        {
            _userService = loginService;
            _loggedInUsersRepository = loggedInUsersRepository;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [RelayCommand]
        public async Task Login()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentException(UserName, nameof(UserName));
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException(Password, nameof(Password));
            }

            var user = await _userService.Login(UserName, Password);

            if (Preferences.ContainsKey(nameof(App.User)))
            {
                Preferences.Remove(nameof(App.User));
            }

            var usedDetails = JsonSerializer.Serialize(user);
            Preferences.Set(nameof(App.User), usedDetails);
            App.User = user;

            var loggedInUser = new LoggedInUser
            {
                Id = user.Id,
                Username = user.Username,
                IsLoggedIn = true
            };
            await _loggedInUsersRepository.SaveUserAsync(loggedInUser);

            await Shell.Current.GoToAsync("//Home");
        }

        [RelayCommand]
        public async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("//Register");
        }

        public async void OnAppearing()
        {
            var users = await _loggedInUsersRepository.GetUsersAsync();
            var loggedInUser = users.SingleOrDefault(user => user.IsLoggedIn);

            if (loggedInUser is not null)
            {
                var user = new User
                {
                    Id = loggedInUser.Id,
                    Username = loggedInUser.Username
                };
                var usedDetails = JsonSerializer.Serialize(user);
                Preferences.Set(nameof(App.User), usedDetails);
                App.User = user;

                await Shell.Current.GoToAsync("//Home");
            }

            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
