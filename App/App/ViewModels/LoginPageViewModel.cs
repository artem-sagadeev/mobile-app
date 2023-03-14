using App.Pages;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace App.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public LoginPageViewModel(IUserService loginService)
        {
            _userService = loginService;
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

            await Shell.Current.GoToAsync("//Home");
        }

        [RelayCommand]
        public async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("//Register");
        }

        public void OnAppearing()
        {
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
