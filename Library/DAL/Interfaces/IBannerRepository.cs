using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public interface IBannerRepository : IDisposable
    {
        void AddBanner(BannerInfo banner);

        BannerInfo? DetailBanner(int id);

        void UpdateBanner(BannerInfo banner);

        void RemoveBanner(int bannerId);

        List<BannerInfo> GetBanners();

        void Save();
    }
}