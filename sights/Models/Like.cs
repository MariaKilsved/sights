using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Like
{
    public byte[] Id { get; set; } = null!;

    public long? UserId { get; set; }

    public long? AttractionId { get; set; }

    public byte[]? Like1 { get; set; }
}
