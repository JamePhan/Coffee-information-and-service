import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import { Button, Col, message, Popconfirm, Row, Space, Table } from 'antd';
import Search from 'antd/lib/input/Search';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { IEvent } from 'src/shared/types/event.type';
import { eventService } from 'src/shared/services/event.service';
import { formattedDate } from '@/utils/functions/convertDay';
import { eventData } from '@/mocks/event';
import FormEvent from './form';

type Props = {};

const EventManagement = ({}: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataEvent, refetch } = useQuery(['listEvent'], () => eventService.getAllEvent());
  const deleteMutation = useMutation({
    mutationKey: ['deleteEventMutation'],
    mutationFn: (eventId: number) => eventService.deleteEvent(eventId),
    onSuccess: () => {
      message.success('Xoá thành công');
      refetch();
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });

  const columns: ColumnType<IEvent>[] = [
    {
      title: '#',
      key: 'id',
      render: (_, record, index) => (
        <div>
          <p>{index}</p>
        </div>
      ),
    },
    {
      title: 'Tên sự kiện',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Tên địa điểm',
      dataIndex: 'locationId',
      key: 'locationId',
    },
    {
      title: 'Ngày',
      key: 'date',
      render: (_, record) => <p>{formattedDate(record.date)}</p>,
    },
    {
      title: 'Bắt đầu lúc',
      key: 'startTime',
      render: (_, record) => <p>{formattedDate(record.startTime)}</p>,
    },
    {
      title: 'Kết thúc lúc',
      key: 'endTime',
      render: (_, record) => <p>{formattedDate(record.endTime)}</p>,
    },
    {
      title: 'Mô tả',
      dataIndex: 'description',
      render: (_, record) => (
        <div className='flex flex-col justify-start items-start gap-3'>
          <p>Mô tả: {record.description}</p>
          <p>Chỗ ngồi: {record.seatCount}</p>
          <p>Giá vé: {record.price}</p>
        </div>
      ),
    },
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <Space size='middle'>
          <div
            className='cursor-pointer'
            onClick={() => {
              setAtion('edit');
              setOpen(true);
              setRowId(record.eventId);
            }}
          >
            <EditOutlined />
          </div>
          <Popconfirm
            okButtonProps={{ loading: deleteMutation.isLoading }}
            onConfirm={() => {
              deleteMutation.mutate(record.eventId);
            }}
            title={'Xoá'}
          >
            <DeleteOutlined className='cursor-pointer'></DeleteOutlined>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <>
      {eventData && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl'>Quản lý sự kiện</h1>
            </Col>
            <Col span={12}>
              <div className='flex py-2 justify-between items-center gap-3'>
                <Search className='bg-blue-300 rounded-lg' placeholder='Tìm kiếm' onSearch={() => {}} enterButton />
                <Button
                  onClick={() => {
                    setAtion('create');
                    setRowId(NaN);
                    setOpen(true);
                  }}
                >
                  Tạo mới
                </Button>
              </div>
            </Col>
          </Row>
          <Table dataSource={eventData} columns={columns} />
          {action === 'create' && !rowId ? (
            <FormEvent refetch={refetch} open={open} setOpen={setOpen} />
          ) : (
            <FormEvent refetch={refetch} editId={rowId} open={open} setOpen={setOpen} />
          )}
        </>
      )}
    </>
  );
};
EventManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default EventManagement;
