using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class EventInfo
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public int? Location { get; set; }
        public DateTime? Date { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? SeatCount { get; set; }
        public decimal? Price { get; set; }
        public int? UserId { get; set; }
    }
}