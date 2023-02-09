using sqlite.Models;

namespace sights.Models
{
    public class CommentUser
    {
        public Comment Comment { get; set; } = new Comment();
        public string? Username { get; set; }
    }
}
