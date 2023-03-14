using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace App.ViewModels
{
    public partial class AddPageViewModel : BaseViewModel
    {
        private readonly IPostService _postService;

        public AddPageViewModel(IPostService postService)
        {
            _postService = postService;
        }

        [ObservableProperty]
        private string _postTitle;

        [ObservableProperty]
        private string _postText;

        [RelayCommand]
        public async Task AddPost()
        {
            await _postService.AddPost(PostTitle, PostText);

            await Shell.Current.GoToAsync("//Profile");
        }

        public void OnAppearing()
        {
            PostTitle = string.Empty;
            PostText = string.Empty;
        }
    }
}
