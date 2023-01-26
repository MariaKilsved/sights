using sqlite.Models;

namespace sights.Models
{
    public class AttractionLike
    {
        public long AttractionId { get; set; }

        public double? Coordinates { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public long? UserId { get; set; }

        public byte[]? Picture { get; set; }

        public long? CountryId { get; set; }

        public long? CityId { get; set; }

        public virtual City? City { get; set; }

        public long LikeId { get; set; }

        public long? Like1 { get; set; }

        public virtual Attraction? Attraction { get; set; }

        public virtual User? User { get; set; }
    }
}
