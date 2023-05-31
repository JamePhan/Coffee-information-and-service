using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? Name { get; set; }

    public int? LocationId { get; set; }

    public DateTime? Date { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? SeatCount { get; set; }

    public decimal? Price { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User? User { get; set; }
}
