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
            .OrderByDescending(post => post.PostDateTime)
            .ToListAsync();

        return posts;
    }

    public async Task AddPost(AddPostDto dto)
    {
        var post = new Post
        {
            Title = dto.Title,
            Text = dto.Text,
            UserId = dto.UserId,
            PostDateTime = DateTime.UtcNow
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

    public async Task<List<Post>> GetFeed(int userId)
    {
        var subscriptions = await _context.Subscriptions
            .Where(subscription => subscription.SubscriberId == userId)
            .Select(subscription => subscription.SubscribedToId)
            .ToListAsync();

        var posts = await _context.Posts
            .Where(post => subscriptions.Contains(post.UserId))
            .OrderByDescending(post => post.PostDateTime)
            .ToListAsync();

        return posts;
    }
}