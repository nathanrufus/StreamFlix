using System.ComponentModel.DataAnnotations;

namespace StreamFlix.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
