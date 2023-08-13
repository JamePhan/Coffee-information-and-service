using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class LocationInfo
    {
        public int LocationId { get; set; }

        public string? Address { get; set; }

        public int? UserId { get; set; }
    }
}