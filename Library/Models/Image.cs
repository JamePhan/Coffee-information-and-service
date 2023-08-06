using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public string? Image1 { get; set; }

    public virtual ICollection<GroupImage> GroupImages { get; set; } = new List<GroupImage>();
}
