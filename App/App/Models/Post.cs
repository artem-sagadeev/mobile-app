using SQLite;

namespace App.Models
{
    public class Post
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }
    }
}
