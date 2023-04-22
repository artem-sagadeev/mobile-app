using App.Models;
using App.Repositories;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        private readonly IPostService _postService;
        private readonly LoggedInUsersRepository _loggedInUsersRepository;

        public ProfilePageViewModel(IPostService postService, LoggedInUsersRepository loggedInUsersRepository)
        {
            _postService = postService;
            _loggedInUsersRepository = loggedInUsersRepository;
        }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private List<Post> _posts;

        public async Task OnAppearing()
        {
            UserName = App.User.Username;
            Posts = await _postService.GetUserPosts(App.User.Id);
        } 

        [RelayCommand]
        public async Task Logout()
        {
            var loggedInUser = await _loggedInUsersRepository.GetUserAsync(App.User.Username);
            await _loggedInUsersRepository.DeleteUserAsync(loggedInUser);

            if (Preferences.ContainsKey(nameof(App.User)))
            {
                Preferences.Remove(nameof(App.User));
            }
            App.User = null;

            await Shell.Current.GoToAsync($"//Login");
        }

        [RelayCommand]
        public async Task Delete(Post post)
        {
            await _postService.DeletePost(post);
            Posts = await _postService.GetUserPosts(App.User.Id);
        }
    }
}
