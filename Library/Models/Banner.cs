using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Banner
{
    public int BannerId { get; set; }

    public int? UserId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual User? User { get; set; }
}
