using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class GroupImage
{
    public int GroupImageId { get; set; }

    public int? ImageId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual Image? Image { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
