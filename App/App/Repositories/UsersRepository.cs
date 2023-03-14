using App.Models;
using SQLite;

namespace App.Repositories
{
    public class UsersRepository
    {
        private SQLiteAsyncConnection _connection;

        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _connection.CreateTableAsync<User>();

            var admin = new User
            {
                Id = 1,
                Username = "admin",
                Password = "admin"
            };
            await _connection.InsertAsync(admin);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            await Init();

            return await _connection.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            await Init();

            return await _connection.Table<User>().FirstOrDefaultAsync(user => user.Username == username && user.Password == password);
        }

        public async Task<int> SaveUserAsync(User user)
        {
            await Init();

            if (user.Id != 0)
                return await _connection.UpdateAsync(user);
            else
                return await _connection.InsertAsync(user);
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            await Init();

            return await _connection.DeleteAsync(user);
        }
    }
}
