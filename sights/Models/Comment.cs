using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Comment
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string? Content { get; set; }

    public long? AttractionId { get; set; }

    public virtual Attraction? Attraction { get; set; }

    public virtual ICollection<SubComment> SubComments { get; } = new List<SubComment>();

    public virtual User? User { get; set; }
}
