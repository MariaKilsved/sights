using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class City
{
    public byte[] Id { get; set; } = null!;

    public long? UserId { get; set; }

    public string? Name { get; set; }

    public long? CountryId { get; set; }
}
