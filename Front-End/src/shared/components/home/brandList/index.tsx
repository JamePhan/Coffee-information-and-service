import React, { useState, useEffect } from 'react';
import { Button, message } from 'antd';
import { useQuery, useMutation } from '@tanstack/react-query';
import { followingService } from 'src/shared/services/following.service';
import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';
import { PreImage } from '@/components/common/PreImage';
import { useAppSelector } from '@/hooks/useRedux';
import Following from '../following';

interface Props {
  brandsData: IInforUser[];
}

const BrandList = ({ brandsData }: Props) => {
  const [userType, setUserType] = useState<string>('User');
  const user = useAppSelector(state => state.appSlice.user);

  const { data: brandData } = useQuery(['listBrands'], () => {
    return userService.getAllUser()
      .then((response) => response.data);
  });

  const followMutation = useMutation(followingService.newFollowing, {
    onSuccess: () => {
      message.success('Theo dõi thành công');
    },
    onError: () => {
      message.error('Theo dõi không thành công');
    },
  });

  const unfollowMutation = useMutation(followingService.unfollow, {
    onSuccess: () => {
      message.success('Hủy theo dõi thành công');
    },
    onError: () => {
      message.error('Hủy theo dõi không thành công');
    },
  });

  function handleFollowClick(brandId: number, isFollowing: boolean): void {
    if (!user) {
      message.warning('Vui lòng đăng nhập để theo dõi.');
      return;
    }

    const brand = brandData.find((brand) => brand.userId === brandId);

    if (!brand) {
      message.error('Không tìm thấy thông tin người dùng.');
      return;
    }

    const followingData: IFollowingAdd = {
      followingId: '0',
      customer: {
        customerId: '0',
        name: 'Tên khách hàng',
        phone: 'Số điện thoại khách hàng',
        address: 'Địa chỉ khách hàng',
        email: 'Email khách hàng',
        avatar: 'URL hình đại diện khách hàng',
      },
      user: {
        userId: '0',
      },
    };

    if (isFollowing) {
      unfollowMutation.mutate(followingData);
    } else {
      followMutation.mutate(followingData);
    }
  }

  return (
    <section className='w-full flex flex-col justify-around items-center mx-auto px-4 md:px-12 lg:px-32 pb-24'>
      <div className='relative w-full mt-5 pb-32 grid grid-cols-1 sm:gird-cols-2 md:grid-cols-3 lg:grid-cols-4 items-start justify-between gap-10'>
        {brandData ? (
          brandData.map((brand, idx) => (
            <div className='mt-5' key={idx}>
              <PreImage
                src={brand.avatar}
                height={200}
                width={200}
                layer={false}
                alt={brand.coffeeShopName}
                className='rounded-md cursor-pointer object-cover border-2 light:border-slate-700 border-slate-100'
              />
              <div className='w-full pt-15 flex justify-between items-center gap-5 light:text-black'>
                <div className='w-full flex flex-col justify-start items-start gap-3'>
                  <p className='font-medium text-2xl'>
                    Tên: {brand.coffeeShopName}
                  </p>
                  <p className='font-thin text-sm'>
                    Địa chỉ: {brand.address}
                  </p>
                  <p className='font-thin text-sm'>
                    Email: {brand.email}
                  </p>
                  <p className='font-thin text-sm'>
                    Sđt: {brand.phone}
                  </p>
                  <Button className='dark:text-white' onClick={() => handleFollowClick(brand.userId, brand.isFollowing)}>
                     {brand.isFollowing ? 'Hủy theo dõi' : 'Theo dõi'}
                  </Button>
                </div>
              </div>
            </div>
          ))
        ) : (
          <p>Loading...</p>
        )}
      </div>
    </section>
  );
};

export default BrandList;
