using System.ComponentModel.DataAnnotations;

namespace MPD_Manjineanu_Mihai.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
