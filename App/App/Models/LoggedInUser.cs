using SQLite;

namespace App.Models
{
    public class LoggedInUser
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Username { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
