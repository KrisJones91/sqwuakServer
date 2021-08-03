namespace sqwuakServer.Models
{
    public class PostArchive
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public int PostId { get; set; }
        public int ArchiveId { get; set; }
    }
}