import { PreImage } from '@/components/common/PreImage';
import { useMutation } from '@tanstack/react-query';
import { Button, message } from 'antd';
import { useState } from 'react';
import { followingService } from 'src/shared/services/following.service';
import { IFollowing, IFollowingAdd } from 'src/shared/types/following.type';

interface Props {
  followingData: IFollowing[];
  userType: string;
}
const Following = ({ followingData, userType }: Props) => {
  const followingShop = useMutation({
    mutationKey: ['followingShopMutaion'],
    mutationFn: (body: IFollowingAdd) => followingService.newFollowing(body),
    onSuccess: () => {
      message.success('Xoá thành công');
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });
  return (
    <section
      id='Following'
      className='w-full flex flex-col justify-around items-center mx-auto px-4 md:px-12 lg:px-32 pb-24'
    >
      <div className='relative w-full mt-5 pb-32 grid grid-cols-1 sm:gird-cols-2 md:grid-cols-3 lg:grid-cols-4 items-start justify-between gap-10'>
        {followingData.map((item, idx) => (
          <div className='mt-5' key={idx}>
            <PreImage
              src={userType === 'Customer' ? item.user.avatar : item.customer.avatar}
              height={200}
              width={200}
              layer={false}
              alt={'Service'}
              className={`rounded-md cursor-pointer object-cover border-2 light:border-slate-700 border-slate-100`}
            />
            <div className='w-full pt-15 flex justify-between items-center gap-5 light:text-black'>
              <div className='w-full flex flex-col justify-start items-start gap-3'>
                <p className='font-medium text-2xl'>
                  Tên: {userType === 'Customer' ? item.user.coffeeShopName : item.customer.name}
                </p>
                <p className='font-thin text-sm'>
                  Địa chỉ: {userType === 'Customer' ? item.user.address : item.customer.address}
                </p>
                <p className='font-thin text-sm'>
                  Email: {userType === 'Customer' ? item.user.email : item.customer.email}
                </p>
                <p className='font-thin text-sm'>
                  Sđt: {userType === 'Customer' ? item.user.phone : item.customer.phone}
                </p>
                {userType === 'Customer' && <Button className='dark:text-white' onSubmit={() => {}}>Yêu thích</Button>}
              </div>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};

export default Following;
