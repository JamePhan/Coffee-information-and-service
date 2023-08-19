import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { DeleteOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, message, Popconfirm, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import FormLocation from './form';
import { locationService } from 'src/shared/services/location.service';
import { ILocation } from 'src/shared/types/location.type';
import { useAppSelector } from '@/hooks/useRedux';

type Props = {};

const LocationManagement = ({}: Props) => {
  const { user } = useAppSelector(state => state.appSlice);
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataLocation, refetch } = useQuery(['listLocation'], () => locationService.getAllLocation(), {
    select: data => {
      const filterData = data.data.filter(item => item.userId === Number(user?.id));
      return filterData;
    },
  });
  const deleteMutation = useMutation({
    mutationKey: ['deleteLocation'],
    mutationFn: (id: number) => locationService.deleteLocation(id),
    onSuccess: () => {
      message.success('Xoá thành công');
      refetch();
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });

  const columns: ColumnType<ILocation>[] = [
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
      title: 'Tên',
      dataIndex: 'address',
      key: 'address',
    },
    {
      title: 'Người đăng',
      dataIndex: 'userId',
      key: 'userId',
    },
    {
      title: 'Hành động',
      key: 'action',
      render: (_, record) => (
        <Space size='middle'>
          <Popconfirm
            okButtonProps={{ loading: deleteMutation.isLoading }}
            onConfirm={() => {
              deleteMutation.mutate(record.locationId);
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
      {dataLocation && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl  text-black'>Quản lý địa điểm</h1>
            </Col>
            <Col span={12}>
              <div className='flex py-2 justify-end items-end gap-3'>
                <Button
                  onClick={() => {
                    refetch()
                  }}
                  icon={<ReloadOutlined className='text-xs' />}
                >
                  Làm mới
                </Button>
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
          <Table dataSource={dataLocation} columns={columns} />
          {action === 'create' && !rowId ? (
            <FormLocation refetch={refetch} open={open} setOpen={setOpen} />
          ) : (
            <FormLocation refetch={refetch} editId={rowId} open={open} setOpen={setOpen} />
          )}
        </>
      )}
    </>
  );
};
LocationManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default LocationManagement;
