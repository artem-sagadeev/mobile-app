using App.Models;

namespace App.Services
{
    public interface IUserService
    {
        Task<User> Login(string username, string password);

        Task<int> Register(User user);

        Task<List<User>> Search(int userId, string searchTerm);

        Task Subscribe(int subscribeToId);

        Task Unsubscribe(int subscribeToId);

        Task<List<int>> GetSubscriptionsIds(int userId);
    }
}
