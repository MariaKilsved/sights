using sqlite.Models;

namespace sights.Models
{
    public class AttractionLike
    {
        public Attraction Attraction { get; set; } = new Attraction();

        public long LikeCount { get; set; }
    }
}
