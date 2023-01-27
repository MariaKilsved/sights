using sqlite.Models;

namespace sights.Models
{
    public class AttractionBase64
    {
        public long Id { get; set; }

        public double? Coordinates { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public long? UserId { get; set; }

        public string? Picture { get; set; }

        public long? CountryId { get; set; }

        public long? CityId { get; set; }
    }
}
