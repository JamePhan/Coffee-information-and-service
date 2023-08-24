import TitleSection from '@/components/common/TitleSection';
import IconLocation from '@/components/icon/event/IconLocation';
import IconSchedule from '@/components/icon/event/IconSchedule';
import { formattedDate } from '@/utils/functions/convertDay';
import { motion, AnimatePresence } from 'framer-motion';
import { ISchedule } from 'src/shared/types/schedule.type';
import { PreImage } from '../../common/PreImage';
import { Button, message } from 'antd';
import { useMutation } from '@tanstack/react-query';
import { eventService } from 'src/shared/services/event.service';
import { scheduleService } from 'src/shared/services/schedule.service';

interface Props {
  scheduleData: ISchedule[];
  userType: string;
}
const Schedule = ({ userType, scheduleData }: Props) => { 
  const deleteMutation = useMutation({
    mutationKey: ['deleteEventMutation'],
    mutationFn: (eventId: number) => scheduleService.deleteSchedule(eventId),
    onSuccess: () => {
      message.success('Xoá thành công');
    
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });
  return (
    <section
      id='Event'
      className='w-full flex flex-col justify-around items-center mx-auto px-4 md:px-12 lg:px-32 pb-24'
    >
      <div className='w-full min-h-[700px] flex flex-col justify-around items-center gap-10'>
        <TitleSection
          title='Lịch trình'
          description={userType === 'Customer' ? 'Lịch đã đặt' : 'Quản lý lịch trình'}
          findMore={false}
        />
        <div className='w-full grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 justify-between items-start gap-5'>
          <AnimatePresence>
            {scheduleData &&
              scheduleData.map((item, idx) => {
                return (
                  <div key={idx} className='relative cursor-pointer overflow-hidden rounded-lg'>
                    <motion.div whileHover={{ scale: 1.1 }}>
                      <PreImage
                        src={item.event.imageUrl}
                        height={250}
                        width={500}
                        layer={false}
                        alt={'Event'}
                        className='w-full rounded-lg object-cover'
                      />
                      <div className='absolute top-0 left-0 p-3 dark:bg-green-700 bg-green-300 rounded-r-lg'>
                        {item.ticketCount} {userType === 'Customer' ? `vé/ ${item.event.price} VND` : `/${item.event.seatCount} chỗ`}
                      </div>
                    </motion.div>
                    <div className=' w-full h-1/2 dark:bg-[#1B1D35] bg-slate-100 p-5 flex flex-col justify-between items-start gap-3'>
                      <div className='flex flex-col justify-start items-start'>
                        <h3>{item.event.name}</h3>
                        <p className='mt-3 flex justify-center items-center gap-3 pl-5 text-[#92400e]'>
                          <span className='mr-2'>
                            <IconSchedule />
                          </span>
                          {formattedDate(item.event.date)}
                        </p>
                        <p className='mt-3 flex justify-center items-center gap-3 pl-5 text-[#92400e]'>
                          <span className='mr-2'>
                            <IconLocation />
                          </span>
                          <p>Địa chỉ {item.event.address}</p>
                        </p>
                      </div>
                      <h1 className='text-xl min-w-1/2 h-[400px] hover:overflow-y-auto'>{item.event.description}</h1>
                      <Button className='float-right dark:text-white' onClick={(()=>deleteMutation.mutate(item.event.eventId))}>
                        Cancel Event
                      </Button>
                    </div>
                  </div>
                );
              })}
          </AnimatePresence>
        </div>
      </div>
    </section>
  );
};

export default Schedule;
