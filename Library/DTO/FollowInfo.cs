﻿using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class FollowInfo
    {
        public int? FollowingId { get; set; }

        public virtual CustomerInfo? Customer { get; set; }

        public virtual UserInfo? User { get; set; }
    }
}