using System.ComponentModel.DataAnnotations;

namespace StreamFlix.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
