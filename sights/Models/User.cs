using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class User
{
    public long Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Attraction> Attractions { get; } = new List<Attraction>();

    public virtual ICollection<City> Cities { get; } = new List<City>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Country> Countries { get; } = new List<Country>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual ICollection<SubComment> SubComments { get; } = new List<SubComment>();
}
