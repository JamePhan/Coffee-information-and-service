import ModalConfirm from '@/components/common/ModalConfirm';
import { PreImage } from '@/components/common/PreImage';
import IconLocation from '@/components/icon/event/IconLocation';
import IconSchedule from '@/components/icon/event/IconSchedule';
import { eventData } from '@/mocks/event';
import { APP_SAVE_KEY } from '@/utils/constants';
import { formattedDate } from '@/utils/functions/convertDay';
import { useQuery } from '@tanstack/react-query';
import { Button } from 'antd';
import { getCookie } from 'cookies-next';
import Head from 'next/head';
import { useRouter } from 'next/router';
import { useState } from 'react';
import { eventService } from 'src/shared/services/event.service';

const EventDetail = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { query } = useRouter();
  const router = useRouter();
  const isLogin = getCookie(APP_SAVE_KEY.LOGIN_STATUS);
  const isIdQuery = typeof query.id === 'string';
  const { data } = useQuery(['eventDetail'], () => eventService.getEventById(query.id as unknown as number), {
    enabled: isIdQuery,
  });

  const handleBook = () => {
    if (isLogin) {
      setIsModalOpen(true);
    } else {
      return router.push('/login');
    }
  };
  return (
    <>
      <Head>
        <title>Sự kiện Coffee Shop</title>
        <meta name='description' content='Sự kiện Coffee Shop' />
        <meta name='keywords' content='Coffee Shop' />
      </Head>
      {query.id && (
        <div className='w-full h-full flex flex-col justify-start items-start  dark:bg-[#1B1D35] bg-slate-100 pb-12'>
          <div className='relative w-full flex justify-between items-center mx-auto'>
            <div className='relative w-full flex-shrink-0 snap-start'>
              <PreImage
                src={eventData[query.id as unknown as number].imageUrl}
                height={400}
                width={1980}
                layer={false}
                alt={'Banner Event'}
                className='w-full object-cover'
              />
            </div>
          </div>
          <div className=' w-full h-1/2 p-5 flex flex-col justify-between items-start gap-3'>
            <div className='flex flex-col justify-start items-start'>
              <h3 className='text-3xl'>Sự kiện: {eventData[query.id as unknown as number].name}</h3>
              <p className='mt-3 flex justify-center items-center gap-3 pl-5 text-[#92400e]'>
                <span className='mr-2'>
                  <IconSchedule />
                </span>
                {formattedDate(eventData[query.id as unknown as number].date)}
              </p>
              <p className='mt-3 flex justify-center items-center gap-3 pl-5 text-[#92400e]'>
                <span className='mr-2'>
                  <IconLocation />
                </span>
                <p>Địa chỉ {eventData[query.id as unknown as number].address}</p>
              </p>
            </div>
            <h1 className='text-2xl min-w-1/2'>
              {eventData[query.id as unknown as number].description.length > 22
                ? eventData[query.id as unknown as number].description.substring(0, 20) + '...'
                : eventData[query.id as unknown as number].description}
            </h1>
            <div className='flex flex-col justify-start items-start gap-3'>
              <p>Chỗ ngồi: {eventData[query.id as unknown as number].seatCount}</p>
              <p>Giá: {eventData[query.id as unknown as number].price} VND</p>
            </div>
            <Button className='float-right' onClick={handleBook}>
              Đăng ký
            </Button>
            <ModalConfirm setIsModalOpen={setIsModalOpen} isModalOpen={isModalOpen} />
          </div>
        </div>
      )}
    </>
  );
};
export default EventDetail;
