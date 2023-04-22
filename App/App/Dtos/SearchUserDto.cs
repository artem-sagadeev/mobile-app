namespace App.Dtos
{
    public class SearchUserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public bool IsSubscribedTo { get; set; }

        public bool IsNotSubscribedTo { get; set; }
    }
}
