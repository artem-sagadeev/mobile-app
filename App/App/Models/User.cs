using SQLite;

namespace App.Models
{
    public class User
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
