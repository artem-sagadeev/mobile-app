using SQLite;

namespace App
{
    public static class Constants
    {
        public const string DatabaseFileName = "App.db3";

        public const SQLiteOpenFlags Flags = 
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}
