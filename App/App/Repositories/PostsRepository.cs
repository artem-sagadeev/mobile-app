using App.Models;
using SQLite;

namespace App.Repositories
{
    public class PostsRepository
    {
        private SQLiteAsyncConnection _connection;

        async Task Init()
        {
            if (_connection is not null)
                return;

            _connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await _connection.CreateTableAsync<Post>();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            await Init();

            return await _connection.Table<Post>().ToListAsync();
        }

        public async Task<List<Post>> GetPostsAsync(int userId)
        {
            await Init();

            return await _connection.Table<Post>().Where(post => post.UserId == userId).ToListAsync();
        }

        public async Task<Post> GetPostAsync(int id)
        {
            await Init();

            return await _connection.Table<Post>().FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<int> SavePostAsync(Post post)
        {
            await Init();

            if (post.Id != 0)
                return await _connection.UpdateAsync(post);
            else
                return await _connection.InsertAsync(post);
        }

        public async Task<int> DeletePostAsync(Post post)
        {
            await Init();

            return await _connection.DeleteAsync(post);
        }
    }
}
