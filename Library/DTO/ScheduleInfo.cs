using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class ScheduleInfo
    {
        public int ScheduleId { get; set; }

        public int? EventId { get; set; }

        public int? CustomerId { get; set; }

        public int? TicketCount { get; set; }
    }
}