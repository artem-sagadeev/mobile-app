using App.Models;
using SQLite;

namespace App.Repositories
{
    public class LoggedInUsersRepository
    {
        private SQLiteAsyncConnection _connection;

        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _connection.CreateTableAsync<LoggedInUser>();
        }

        public async Task<List<LoggedInUser>> GetUsersAsync()
        {
            await Init();

            return await _connection.Table<LoggedInUser>().ToListAsync();
        }

        public async Task<LoggedInUser> GetUserAsync(string username)
        {
            await Init();

            return await _connection.Table<LoggedInUser>().FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<int> SaveUserAsync(LoggedInUser user)
        {
            await Init();

            return await _connection.InsertAsync(user);
        }

        public async Task<int> DeleteUserAsync(LoggedInUser user)
        {
            await Init();

            return await _connection.DeleteAsync(user);
        }
    }
}
