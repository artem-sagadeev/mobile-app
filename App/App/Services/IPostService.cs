using App.Models;

namespace App.Services
{
    public interface IPostService
    {
        Task<Post> GetPost(int postId);

        Task<List<Post>> GetUserPosts(int userId);

        Task AddPost(string title, string text);

        Task DeletePost(Post post);
    }
}
