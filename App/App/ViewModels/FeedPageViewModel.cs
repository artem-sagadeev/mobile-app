using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels
{
    public partial class FeedPageViewModel : BaseViewModel
    {
        private readonly IPostService _postService;

        public FeedPageViewModel(IPostService postService)
        {
            _postService = postService;
        }

        [ObservableProperty]
        private List<Post> _posts;

        public async Task OnAppearing()
        {
            Posts = await _postService.GetFeed(App.User.Id);
        }
    }
}
