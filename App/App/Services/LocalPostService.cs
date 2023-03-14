using App.Models;
using App.Repositories;

namespace App.Services
{
    public class LocalPostService : IPostService
    {
        private readonly PostsRepository _postsRepository;

        public LocalPostService(PostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<List<Post>> GetUserPosts(int userId)
        {
            return await _postsRepository.GetPostsAsync(userId);
        }

        public async Task AddPost(string title, string text)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException(title, nameof(title));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException(text, nameof(text));
            }

            var post = new Post
            {
                Title = title,
                Text = text,
                UserId = App.User.Id
            };

            await _postsRepository.SavePostAsync(post);
        }

        public async Task DeletePost(Post post)
        {
            await _postsRepository.DeletePostAsync(post);
        }
    }
}
