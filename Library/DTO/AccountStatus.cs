using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class AccountStatus
    {
        public int AccountId { get; set; }
        public bool? IsBanned { get; set; }
        public string? AccountImage { get; set; }
    }
}