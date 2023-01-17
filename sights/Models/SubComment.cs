using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class SubComment
{
    public byte[] Id { get; set; } = null!;

    public long? CommentId { get; set; }

    public long? UserId { get; set; }

    public string? Content { get; set; }
}
