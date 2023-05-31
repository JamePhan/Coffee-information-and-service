using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int? EventId { get; set; }

    public int? CustomerId { get; set; }

    public int? TicketCount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Event? Event { get; set; }
}
