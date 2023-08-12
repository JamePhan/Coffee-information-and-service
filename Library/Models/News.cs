using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class News
{
    public int NewsId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? GroupImageId { get; set; }

    public virtual GroupImage? GroupImage { get; set; }

    public virtual User? User { get; set; }
}
