using System.ComponentModel.DataAnnotations;

namespace sqwuakServer.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Shares { get; set; }
        public int Saves { get; set; }
        public Account Creator { get; set; }
    }

    public class PostArchModel : Post
    {
        public int ArchPostId { get; set; }
    }
}