using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DTO
{
    public class BannerInfo
    {
        public int BannerId { get; set; }
        public int? UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}