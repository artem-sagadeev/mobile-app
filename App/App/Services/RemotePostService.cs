using App.Dtos;
using App.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace App.Services
{
    public class RemotePostService : IPostService
    {
        private static readonly string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android
            ? "https://10.0.2.2:7077/posts/" : "https://127.0.0.1:7077/posts/";

        private readonly HttpClient _httpClient;

        public RemotePostService()
        {
#if DEBUG
            _httpClient = new HttpClient(HttpsClientHandlerService.GetPlatformMessageHandler());
#else
            _httpClient = new HttpClient();
#endif
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            var url = BaseUrl + $"?userId={userId}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(content);

            return posts;
        }

        public async Task AddPost(string title, string text)
        {
            var addPostDto = new AddPostDto
            {
                Title = title,
                Text = text,
                UserId = App.User.Id
            };
            await _httpClient.PostAsJsonAsync<AddPostDto>(BaseUrl, addPostDto);
        }

        public async Task DeletePost(Post post)
        {
            var url = BaseUrl + $"?postId={post.Id}";
            await _httpClient.DeleteAsync(url);
        }

        public Task<Post> GetPost(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetFeed(int userId)
        {
            var url = BaseUrl + $"feed?userId={userId}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<List<Post>>(content);

            return posts;
        }
    }
}
