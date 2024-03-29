﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class NewsInfo
    {
        public int NewsId { get; set; }

        public string CoffeeShopName { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}