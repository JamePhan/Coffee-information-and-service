using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class UserInfo
    {
        public int UserId { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CoffeeShopName { get; set; }

        public int? AccountId { get; set; }
    }
}