import React, { useEffect, useState } from 'react';
import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { CloseCircleOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, Input, Row, Space, Table, message, Tabs } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useMutation, useQuery } from '@tanstack/react-query';
import { userService } from 'src/shared/services/user.service';
import { customerService } from 'src/shared/services/customer.service';
import { IInforUser, IUserbanned } from 'src/shared/types/user.type';
import { ICustomer, ICustomerbanned } from 'src/shared/types/customer.type';
import { PreImage } from '@/components/common/PreImage';

const { TabPane } = Tabs;

type Props = {};

const RestrictUser = ({ }: Props) => {
  const [searchText, setSearchText] = useState('');
  const { data: dataUser, refetch: refetchUser } = useQuery(['bannedUserList'], () => userService.getBannedUsers());
  const { data: dataCustomer, refetch: refetchCustomer } = useQuery(['bannedCustomerList'], () => customerService.getBannedCustomer());
  const [noUserData, setNoUserData] = useState(false);
  const [noCustomerData, setNoCustomerData] = useState(false);

  useEffect(() => {
    if (!dataUser?.data || dataUser.data.length === 0) {
      setNoUserData(true);
    } else {
      setNoUserData(false);
    }
  }, [dataUser]);

  useEffect(() => {
    if (!dataCustomer?.data || dataCustomer.data.length === 0) {
      setNoCustomerData(true);
    } else {
      setNoCustomerData(false);
    }
  }, [dataCustomer]);

  const unbanUserMutation = useMutation((body: IUserbanned) => userService.banUser(body), {
    onSuccess() {
      message.success('Unbanned successfully');
      refetchUser();
    },
    onError(error) {
      console.error('Error unbanning user account', error);
      message.error('Failed to unban user account');
    },
  });

  const unbanCustomerMutation = useMutation((body: ICustomerbanned) => customerService.banCustomer(body), {
    onSuccess() {
      message.success('Unbanned successfully');
      refetchCustomer();
    },
    onError(error) {
      console.error('Error unbanning customer account', error);
      message.error('Failed to unban customer account');
    },
  });

  const columnsUser: ColumnType<IInforUser>[] = [
    {
      title: '#',
      key: 'id',
      render: (_, record, index) => (
        <div>
          <p>{index + 1}</p>
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
            width={50}
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
          <Button
            icon={<CloseCircleOutlined />}
            onClick={() => {
              const body = {
                role: 'user',
                profileId: Number(record?.userId),
              };
              unbanUserMutation.mutate(body);
            }}
            type='primary'
            danger
          >
            Unban
          </Button>
        </Space>
      ),
    },
  ];

  const columnsCustomer: ColumnType<ICustomer>[] = [
    {
      title: '#',
      key: 'id',
      render: (_, record, index) => (
        <div>
          <p>{index + 1}</p>
        </div>
      ),
    },
    {
      title: 'Tên người dùng',
      dataIndex: 'name',
      key: 'name',
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
          <Button
            icon={<CloseCircleOutlined />}
            onClick={() => {
              const body = {
                role: 'customer',
                profileId: Number(record?.customerId),
              };
              unbanCustomerMutation.mutate(body);
            }}
            type='primary'
            danger
          >
            Unban
          </Button>
        </Space>
      ),
    },
  ];

  const filteredUserData = dataUser?.data?.filter((user) =>
    user.coffeeShopName.toLowerCase().includes(searchText.toLowerCase())
  );

  const filteredCustomerData = dataCustomer?.data?.filter((customer: { name: string; }) =>
    customer.name.toLowerCase().includes(searchText.toLowerCase())
  );

  return (
    <>
      <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
        <Col span={12}>
          <h1 className='font-bold text-2xl text-black'>Quản lý Account Banned</h1>
        </Col>
        <Col span={12}>
          <div className='flex py-2 justify-end items-end gap-3'>
            <Input
              placeholder="Tìm kiếm theo tên người dùng"
              value={searchText}
              onChange={(e) => setSearchText(e.target.value)}
              style={{ width: '400px' }}
            />
          </div>
        </Col>
      </Row>
      <Tabs defaultActiveKey='user' tabBarGutter={50}>
        <TabPane tab='User bị Ban' key='user'>
          {noUserData ? (
            <p>Không có users bị ban</p>
          ) : (
            <Table dataSource={filteredUserData} columns={columnsUser} />
          )}
        </TabPane>
        <TabPane tab='Customer bị Ban' key='customer'>
          {noCustomerData ? (
            <p>Không có customers bị ban</p>
          ) : (
            <Table dataSource={filteredCustomerData} columns={columnsCustomer} />
          )}
        </TabPane>
      </Tabs>
    </>
  );
};

RestrictUser.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;

export default RestrictUser;
