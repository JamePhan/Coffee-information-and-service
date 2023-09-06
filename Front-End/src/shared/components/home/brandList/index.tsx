import React, { useState, useEffect } from 'react';
import { Button, message } from 'antd';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { followingService } from 'src/shared/services/following.service';
import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';
import { PreImage } from '@/components/common/PreImage';
import { useAppSelector } from '@/hooks/useRedux';
import { IFollowingAdd } from 'src/shared/types/following.type';

interface Props {
  brandsData: IInforUser[];
}

const BrandList = ({ brandsData }: Props) => {
  const user = useAppSelector(state => state.appSlice.user);
  const queryClient = useQueryClient();

  const { data: brandData, isLoading: brandDataLoading } = useQuery(['listBrands'], () => {
    return userService.getAllUser().then(response => response.data);
  });

  const followMutation = useMutation(
    (followingData: IFollowingAdd) => followingService.newFollowing(followingData),
    {
      onMutate: (followingData) => {
        const updatedBrandData = brandData?.map((brand) => {
          if (brand.userId === Number(followingData.user.userId)) {
            return { ...brand, isFollowing: true };
          }
          return brand;
        });

        queryClient.setQueryData(['listBrands'], updatedBrandData);
      },
      onError: () => {
        message.error('Theo dõi không thành công');
      },
    }
  );

  const unfollowMutation = useMutation<void, unknown, IFollowingAdd>(
    async (followingData) => {
      try {
        await followingService.unfollow(followingData);
      } catch (error) {
        throw error;
      }
    },
    {
      onMutate: (followingData) => {
        const updatedBrandData = brandData?.map((brand) => {
          if (brand.userId === parseInt(followingData.user.userId, 10)) {
            return { ...brand, isFollowing: false };
          }
          return brand;
        });

        queryClient.setQueryData(['listBrands'], updatedBrandData);
      },
      onError: () => {
        message.error('Hủy theo dõi không thành công');
      },
    }
  );

  useEffect(() => {
    const fetchFollowingLists = async () => {
      if (user && !brandDataLoading) {
        const customerList = await followingService.getCustomerList(parseInt(user.profileId, 10));

        const updatedBrandData = brandData?.map((brand) => {
          const isFollowingCustomer = customerList.data.some((item) => item.user.userId === brand.userId);

          return { ...brand, isFollowing: isFollowingCustomer };
        });

        queryClient.setQueryData(['listBrands'], updatedBrandData);
      }
    };

    fetchFollowingLists();
  }, [user, brandData, brandDataLoading, queryClient]);

  const handleFollowClick = async (brandId: number, isFollowing: boolean): Promise<void> => {
    if (!user) {
      message.warning('Vui lòng đăng nhập để theo dõi.');
      return;
    }

    const brand = brandData?.find((brand) => brand.userId === brandId);

    if (!brand) {
      message.error('Không tìm thấy thông tin người dùng.');
      return;
    }

    const followingData: IFollowingAdd = {
      followingId: '0', // Đặt followingId thành 0
      customer: {
        customerId: user.profileId, // Sử dụng ID khách hàng của người dùng đã đăng nhập
      },
      user: {
        userId: brand.userId.toString(), // Sử dụng ID người dùng của thương hiệu
      },
    };

    if (isFollowing) {
      await unfollowMutation.mutateAsync(followingData);
    } else {
      await followMutation.mutateAsync(followingData);
    }
  };


  return (
    <section className='w-full flex flex-col justify-around items-center mx-auto px-4 md:px-12 lg:px-32 pb-24'>
      <div className='relative w-full mt-5 pb-32 grid grid-cols-1 sm:gird-cols-2 md:grid-cols-3 lg:grid-cols-4 items-start justify-between gap-10'>
        {brandDataLoading ? (
          <p>Loading...</p>
        ) : (
          brandData?.map((brand, idx) => (
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
                  <Button
                    className='dark:text-white'
                    onClick={() => handleFollowClick(brand.userId, brand.isFollowing)}
                  >
                    {brand.isFollowing ? 'Đang Theo Dõi' : 'Theo Dõi'}
                  </Button>
                </div>
              </div>
            </div>
          ))
        )}
      </div>
    </section>
  );
};

export default BrandList;
