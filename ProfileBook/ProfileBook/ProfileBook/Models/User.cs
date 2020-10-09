using SQLite;

namespace ProfileBook.Models
{
    [Table("Users")]
    public class User
    {       
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}
