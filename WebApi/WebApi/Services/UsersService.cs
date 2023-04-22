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

    public async Task Register(RegisterDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password,
            IsLoggedIn = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> Search(int userId, string searchTerm)
    {
        var users = await _context.Users
            .Where(user => user.Username.Contains(searchTerm) && user.Id != userId)
            .ToListAsync();

        return users;
    }
}