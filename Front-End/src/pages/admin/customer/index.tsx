import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { StopOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table, Input } from 'antd'; // Thêm Input từ Ant Design
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import FormUser from './form';
import { userService } from 'src/shared/services/user.service';
import { ICustomer } from 'src/shared/types/customer.type';
import { PreImage } from '@/components/common/PreImage';
import { customerService } from 'src/shared/services/customer.service';

type Props = {};

const CustomerManagement = ({ }: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAction] = useState<string>('');
  const [rowId, setRowId] = useState<number>();
  const [searchValue, setSearchValue] = useState('');
  const [originalData, setOriginalData] = useState<ICustomer[]>([]);

  // Hàm xử lý tìm kiếm
  const handleSearch = () => {
    const filteredData = originalData.filter((customer) =>
      customer.name.toLowerCase().includes(searchValue.toLowerCase())
    );
    // Cập nhật danh sách khách hàng sau khi tìm kiếm
    setDataCustomer({ ...dataCustomer, data: filteredData });
  };

  const { data: dataCustomer, refetch, setData: setDataCustomer } = useQuery(
    ['listCustomer'],
    () => {
      // Lấy danh sách khách hàng và cập nhật originalData
      const result = customerService.getAllCustomer();
      setOriginalData(result.data);
      return result;
    }
  );

  const columns: ColumnType<ICustomer>[] = [
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
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Ảnh đại diện',
      dataIndex: 'avatar',
      render: (_, record) => (
        <div className='w-[50px] rounded-lg'>
          <PreImage
            width={1980}
            height={50}
            alt={`Image ${record.customerId}`}
            className='w-full object-cover rounded-full'
            src={''}
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
              setAction('ban');
              setOpen(true);
              setRowId(record.customerId);
            }}
          >
            <StopOutlined /> Ban Account
          </div>
        </Space>
      ),
    },
  ];

  return (
    <>
      {dataCustomer && (
        <>
          {/* Dòng chứa ô tìm kiếm và dòng văn bản */}
          <Row className='mb-12' justify='space-between' align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl text-black'>Quản lý Customer</h1>
            </Col>
            <Col span={12}>
              <div className='flex py-2 justify-end items-end gap-3'>
                {/* Ô tìm kiếm */}
                <Input.Search
                  placeholder='Tìm theo tên người dùng'
                  value={searchValue}
                  onChange={(e) => setSearchValue(e.target.value)}
                  onSearch={handleSearch}
                />

                {/* Nút làm mới */}
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

          <Table dataSource={dataCustomer.data} columns={columns} />
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

CustomerManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;

export default CustomerManagement;
