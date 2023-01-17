using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Comment
{
    public byte[] Id { get; set; } = null!;

    public long? UserId { get; set; }

    public string? Content { get; set; }

    public long? AttractionId { get; set; }
}
