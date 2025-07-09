using SQLite;

namespace w12.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int userId { get; set; }

        [NotNull]   
        public string Name { get; set; } = string.Empty;    
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime ResgistrationDate { get; set; }
    }
}
