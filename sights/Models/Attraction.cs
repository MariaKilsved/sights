using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Attraction
{
    public long Id { get; set; }

    public double? Coordinates { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long UserId { get; set; }

    public byte[]? Picture { get; set; }

    public long CountryId { get; set; }

    public long? CityId { get; set; }

    public virtual City? City { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual User User { get; set; } = null!;
}
