import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';
import { Button, Col, message, Popconfirm, Row, Space, Table } from 'antd';
import Search from 'antd/lib/input/Search';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import FormServiceCoffee from './form';
import { serviceCoffeeService } from 'src/shared/services/serviceCoffee.service';
import { IServiceCoffee } from 'src/shared/types/serviceCoffee.type';
import { serviceCoffeeData } from '@/mocks/serviceCoffee';
import { PreImage } from '@/components/common/PreImage';

type Props = {};

const ServiceCoffeeManagement = ({}: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataServiceCoffee, refetch } = useQuery(['listServiceCoffee'], () =>
    serviceCoffeeService.getAllServiceCoffee(),
  );
  const deleteMutation = useMutation({
    mutationKey: ['deleteServiceCoffee'],
    mutationFn: (id: number) => serviceCoffeeService.deleteServiceCoffee(id),
    onSuccess: () => {
      message.success('Xoá thành công');
      refetch();
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });

  const columns: ColumnType<IServiceCoffee>[] = [
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
      title: 'Tên dịch vụ',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Mô tả',
      dataIndex: 'description',
      key: 'description',
    },
    {
      title: 'Hình ảnh',
      key: 'imageUrl',
      render: (_, record) => (
        <PreImage
          src={record && record.imageUrl}
          alt={`Image ${record.name}`}
          width={200}
          height={200}
          className='w-full object-cover rounded-lg'
        />
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
              setRowId(record.serviceId);
            }}
          >
            <EditOutlined />
          </div>
          <Popconfirm
            okButtonProps={{ loading: deleteMutation.isLoading }}
            onConfirm={() => {
              deleteMutation.mutate(record.serviceId);
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
      {serviceCoffeeData && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl'>Quản lý dịch vụ</h1>
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
          <Table dataSource={serviceCoffeeData} columns={columns} />
          {action === 'create' && !rowId ? (
            <FormServiceCoffee refetch={refetch} open={open} setOpen={setOpen} />
          ) : (
            <FormServiceCoffee refetch={refetch} editId={rowId} open={open} setOpen={setOpen} />
          )}
        </>
      )}
    </>
  );
};
ServiceCoffeeManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default ServiceCoffeeManagement;