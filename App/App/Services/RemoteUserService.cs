using App.Dtos;
using App.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace App.Services
{
    public class RemoteUserService : IUserService
    {
        private static readonly string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android 
            ? "https://10.0.2.2:7077/users/" : "https://127.0.0.1:7077/users/";

        private readonly HttpClient _httpClient;

        public RemoteUserService() 
        {
#if DEBUG
            _httpClient = new HttpClient(HttpsClientHandlerService.GetPlatformMessageHandler());
#else
            _httpClient = new HttpClient();
#endif
        }

        public async Task<User> Login(string username, string password)
        {
            var loginDto = new LoginDto
            {
                Username = username,
                Password = password
            };
            var url = BaseUrl + "login";
            var response = await _httpClient.PostAsJsonAsync(url, loginDto);
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);

            return user;
        }

        public async Task<int> Register(User user)
        {
            var registerDto = new RegisterDto
            {
                Username = user.Username,
                Password = user.Password
            };
            var url = BaseUrl + "register";
            var response = await _httpClient.PostAsJsonAsync(url, registerDto);
            var content = await response.Content.ReadAsStringAsync(); 
            var id = JsonConvert.DeserializeObject<int>(content);

            return id;
        }

        public async Task<List<User>> Search(int userId, string searchTerm)
        {
            var url = BaseUrl + $"search?userId={userId}&searchTerm={searchTerm}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync() ;
            var users = JsonConvert.DeserializeObject<List<User>>(content);

            return users;
        }

        public async Task Subscribe(int subscribeToId)
        {
            var subscribeDto = new SubscribeDto
            {
                SubscriberId = App.User.Id,
                SubscribedToId = subscribeToId
            };
            var url = BaseUrl + "subscribe";
            await _httpClient.PostAsJsonAsync(url, subscribeDto);
        }

        public async Task Unsubscribe(int subscribeToId)
        {
            var subscribeDto = new SubscribeDto
            {
                SubscriberId = App.User.Id,
                SubscribedToId = subscribeToId
            };
            var url = BaseUrl + "unsubscribe";
            await _httpClient.PostAsJsonAsync(url, subscribeDto);
        }

        public async Task<List<int>> GetSubscriptionsIds(int userId)
        {
            var url = BaseUrl + $"subscriptions?userId={userId}";
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var ids = JsonConvert.DeserializeObject<List<int>>(content);

            return ids;
        }
    }
}
