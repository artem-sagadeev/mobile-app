using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginDto dto)
    {
        var user = await _usersService.Login(dto);

        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        await _usersService.Register(dto);
        
        return Ok();
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<User>>> Search(int userId, string searchTerm)
    {
        var users = await _usersService.Search(userId, searchTerm);

        return Ok(users);
    }
}