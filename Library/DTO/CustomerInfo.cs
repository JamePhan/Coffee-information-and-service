﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class CustomerInfo
    {
        public int CustomerId { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Email { get; set; }

        public int? AccountId { get; set; }
    }
}