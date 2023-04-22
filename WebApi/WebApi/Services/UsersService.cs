using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Services;

public class UsersService
{
    private readonly ApplicationContext _context;

    public UsersService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> Login(LoginDto dto)
    {
        var user = await _context.Users
            .Where(user => user.Username == dto.Username && user.Password == dto.Password)
            .SingleOrDefaultAsync();

        return user;
    }

    public async Task<int> Register(RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public async Task<List<User>> Search(int userId, string searchTerm)
    {
        var users = await _context.Users
            .Where(user => user.Username.Contains(searchTerm) && user.Id != userId)
            .ToListAsync();

        return users;
    }

    public async Task Subscribe(SubscribeDto dto)
    {
        var subscription = new Subscription
        {
            SubscriberId = dto.SubscriberId,
            SubscribedToId = dto.SubscribedToId
        };

        _context.Subscriptions.Add(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task Unsubscribe(SubscribeDto dto)
    {
        var subscription = await _context.Subscriptions
            .Where(subscription => subscription.SubscriberId == dto.SubscriberId &&
                                   subscription.SubscribedToId == dto.SubscribedToId)
            .SingleOrDefaultAsync();

        _context.Subscriptions.Remove(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task<List<int>> GetSubscriptionsIds(int userId)
    {
        var subscriptions = await _context.Subscriptions
            .Where(subscription => subscription.SubscriberId == userId)
            .Select(subscription => subscription.SubscribedToId)
            .ToListAsync();

        return subscriptions;
    }
}