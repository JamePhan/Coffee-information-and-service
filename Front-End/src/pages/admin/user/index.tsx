import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { EditOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import FormUser from './form';
import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';
import { PreImage } from '@/components/common/PreImage';

type Props = {};

const UserManagement = ({}: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataUser, refetch } = useQuery(['listUser'], () => userService.getAllUser());
  const columns: ColumnType<IInforUser>[] = [
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
      title: 'Tên người dùng',
      dataIndex: 'coffeeShopName',
      key: 'coffeeShopName',
    },
    {
      title: 'Ảnh dại diện',
      dataIndex: 'avatar',
      render: (_, record) => (
        <div className='w-[50px] rounded-lg'>
          <PreImage
            width={1980}
            height={50}
            alt={`Image ${record.userId}`}
            src={record.avatar}
            className='w-full object-cover rounded-full'
          />
        </div>
      ),
    },
    {
      title: 'Số điện thoại',
      dataIndex: 'phone',
      key: 'phone',
    },
    {
      title: 'Địa chỉ',
      dataIndex: 'address',
      key: 'address',
    },
    {
      title: 'Email',
      dataIndex: 'email',
      key: 'email',
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
              setRowId(record.userId);
            }}
          >
            <EditOutlined />
          </div>
        </Space>
      ),
    },
  ];

  return (
    <>
      {dataUser && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl  text-black'>Quản lý người dùng</h1>
            </Col>
            <Col span={12}>
              <div className='flex py-2 justify-end items-end gap-3'>
              <Button
                  onClick={() => {
                    refetch();
                  }}
                  icon={<ReloadOutlined className='text-xs' />}
                >
                  Làm mới
                </Button>
              </div>
            </Col>
          </Row>
          <Table dataSource={dataUser.data} columns={columns} />
          {action === 'create' && !rowId ? (
            <FormUser refetch={refetch} open={open} setOpen={setOpen} />
          ) : (
            <FormUser refetch={refetch} editId={rowId} open={open} setOpen={setOpen} />
          )}
        </>
      )}
    </>
  );
};
UserManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default UserManagement;
