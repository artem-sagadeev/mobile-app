using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly PostsService _postsService;

    public PostsController(PostsService postsService)
    {
        _postsService = postsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Post>>> GetUserPosts(int userId)
    {
        var posts = await _postsService.GetUserPosts(userId);

        return Ok(posts);
    }

    [HttpPost]
    public async Task<ActionResult> AddPost(AddPostDto dto)
    {
        await _postsService.AddPost(dto);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeletePost(int postId)
    {
        await _postsService.DeletePost(postId);

        return Ok();
    }

    [HttpGet("feed")]
    public async Task<ActionResult<List<Post>>> GetFeed(int userId)
    {
        var posts = await _postsService.GetFeed(userId);

        return posts;
    }
}