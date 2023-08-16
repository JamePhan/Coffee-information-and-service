using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string? Address { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual User? User { get; set; }
}
