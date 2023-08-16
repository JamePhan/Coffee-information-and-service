using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class ServiceInfo
    {
        public int ServiceId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? CoffeeShopName { get; set; }

        public string? ImageUrl { get; set; }
    }
}