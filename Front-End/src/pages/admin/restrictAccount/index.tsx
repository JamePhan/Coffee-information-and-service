import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { CloseCircleOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table, message } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';

import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';
import { PreImage } from '@/components/common/PreImage';

type Props = {};

const UserManagement = ({ }: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAction] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataUser, refetch } = useQuery(['bannedUserList'], () => userService.getBannedUsers());

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
      title: 'Ảnh đại diện',
      dataIndex: 'avatar',
      render: (_, record) => (
        <div className='w-[50px] rounded-lg'>
          <PreImage
            width={50} // Thay đổi chiều rộng thành 50px
            height={50} // Thay đổi chiều cao thành 50px
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
              setAction('unrestrict');
              setOpen(true);
              setRowId(record.userId);
            }}
          >
            <Button
              type='danger'
              icon={<CloseCircleOutlined />}
              onClick={() => {
                userService
                  .banUser(record.userId)
                  .then(() => {
                    message.success('Unbanned successfully');
                    refetch();
                  })
                  .catch((error) => {
                    console.error('Error unbanning account', error);
                    message.error('Failed to unban account');
                  });
              }}
            >
              Unban Account
            </Button>
          </div>
        </Space>
      ),
    },
  ];

  return (
    <>
      {dataUser && dataUser.data && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl  text-black'>Quản lý người dùng bị Ban</h1>
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
        </>
      )}
    </>
  );
};

UserManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;

export default UserManagement;
