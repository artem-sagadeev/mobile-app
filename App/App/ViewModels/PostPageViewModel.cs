using App.Models;
using App.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace App.ViewModels
{
    public partial class PostPageViewModel : BaseViewModel
    {
        private readonly IPostService _postService;

        public PostPageViewModel(IPostService postService)
        {
            _postService = postService;
        }

        [ObservableProperty]
        private Post _post;

        public async Task OnAppearing()
        {
            _postService.GetPost()
        }
    }
}
