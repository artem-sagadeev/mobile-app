using App.Models;
using App.Repositories;

namespace App.Services
{
    public class LocalUserService : IUserService
    {
        private readonly UsersRepository _usersRepository;

        public LocalUserService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _usersRepository.GetUserAsync(username, password);

            return user;
        }

        public async Task Register(User user)
        {
            await _usersRepository.SaveUserAsync(user);
        }
    }
}
