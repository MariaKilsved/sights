using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Country
{
    public byte[] Id { get; set; } = null!;

    public string? Name { get; set; }

    public long? UserId { get; set; }
}
