using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class SubComment
{
    public long Id { get; set; }

    public long? CommentId { get; set; }

    public long? UserId { get; set; }

    public string? Content { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual User? User { get; set; }
}
