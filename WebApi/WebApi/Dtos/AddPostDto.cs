namespace WebApi.Dtos;

public class AddPostDto
{
    public string Title { get; set; } 
    
    public string Text { get; set; }
    
    public int UserId { get; set; }
}