using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class User
{
    public byte[] Id { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }
}
