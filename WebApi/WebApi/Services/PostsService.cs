using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Services;

public class PostsService
{
    private readonly ApplicationContext _context;

    public PostsService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Post>> GetUserPosts(int userId)
    {
        var posts = await _context.Posts
            .Where(post => post.UserId == userId)
            .ToListAsync();

        return posts;
    }

    public async Task AddPost(AddPostDto dto)
    {
        var post = new Post
        {
            Title = dto.Title,
            Text = dto.Text,
            UserId = dto.UserId
        };

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePost(int postId)
    {
        var post = await _context.Posts
            .Where(post => post.Id == postId)
            .SingleOrDefaultAsync();

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}