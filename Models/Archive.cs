namespace sqwuakServer.Models
{
    public class Archive
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public bool isPrivate { get; set; }
        public Account Creator { get; set; }
    }

    public class PostArchiveModel : Posts
    {
        public int PostArchiveId { get; set; }
    }
}