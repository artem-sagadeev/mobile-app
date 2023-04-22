namespace WebApi.Entities;

public class Subscription
{
    public int Id { get; set; }
    
    public int SubscriberId { get; set; }
    
    public User Subscriber { get; set; }
    
    public int SubscribedToId { get; set; }
    
    public User SubscribedTo { get; set; }
}