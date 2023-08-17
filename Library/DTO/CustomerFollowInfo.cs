using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class CustomerFollowInfo
    {
        public int? FollowingId { get; set; }

        public virtual UserInfo? User { get; set; }

        public bool Followed { get; set; }
    }
}