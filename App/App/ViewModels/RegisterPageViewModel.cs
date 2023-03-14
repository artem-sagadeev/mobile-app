using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace App.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public RegisterPageViewModel(IUserService loginService)
        {
            _userService = loginService;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _repeatPassword;

        [RelayCommand]
        public async Task Register()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                throw new ArgumentException(UserName, nameof(UserName));
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException(Password, nameof(Password));
            }

            if (string.IsNullOrEmpty(RepeatPassword))
            {
                throw new ArgumentException(RepeatPassword, nameof(RepeatPassword));
            }

            if (Password != RepeatPassword)
            {
                throw new ArgumentException(RepeatPassword, nameof(RepeatPassword));
            }

            var user = new User
            {
                Username = UserName,
                Password = Password
            };

            await _userService.Register(user);

            if (Preferences.ContainsKey(nameof(App.User)))
            {
                Preferences.Remove(nameof(App.User));
            }

            var usedDetails = JsonSerializer.Serialize(user);
            Preferences.Set(nameof(App.User), usedDetails);
            App.User = user;

            await Shell.Current.GoToAsync($"//Home");
        }

        [RelayCommand]
        public async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("//Login");
        }

        public void OnAppearing()
        {
            UserName = string.Empty;
            Password = string.Empty;
            RepeatPassword = string.Empty;
        }
    }
}
