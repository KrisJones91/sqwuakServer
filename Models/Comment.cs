using System.ComponentModel.DataAnnotations;

namespace sqwuakServer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        [Required]
        public string Body { get; set; }
        public int Likes { get; set; }
        public int PostId { get; set; }
        public Account Creator { get; set; }
    }
}