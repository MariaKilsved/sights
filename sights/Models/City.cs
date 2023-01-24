using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class City
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? UserId { get; set; }

    public long? CountryId { get; set; }

    public virtual ICollection<Attraction> Attractions { get; } = new List<Attraction>();

    public virtual Country? Country { get; set; }

    public virtual User? User { get; set; }
}
