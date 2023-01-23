using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Country
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? UserId { get; set; }

    public virtual ICollection<Attraction> Attractions { get; } = new List<Attraction>();

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual User? User { get; set; }
}
