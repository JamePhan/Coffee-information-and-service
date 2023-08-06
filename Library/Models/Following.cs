using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Following
{
    public int FollowingId { get; set; }

    public int? UserId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual User? User { get; set; }
}
