using System;
using System.Collections.Generic;

namespace sqlite.Models;

public partial class Like
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? AttractionId { get; set; }

    public long? Like1 { get; set; }

    public virtual Attraction? Attraction { get; set; }

    public virtual User? User { get; set; }
}
