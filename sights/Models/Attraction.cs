using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Attraction
{
    public byte[] Id { get; set; } = null!;

    public byte[]? Cordinates { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public long? UserId { get; set; }

    public long? CountryId { get; set; }

    public long? CityId { get; set; }

    public byte[]? Picture { get; set; }
}
