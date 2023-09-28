import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { EditOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table } from 'antd';
import Search from 'antd/lib/input/Search';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { customerService } from 'src/shared/services/customer.service';
import { ICustomer } from 'src/shared/types/customer.type';

type Props = {};

const CustomerManagement = ({ }: Props) => {
  const [searchText, setSearchText] = useState(''); // State to store search text
  const { data: dataCustomer } = useQuery(['listCustomer'], () =>
    customerService.getAllCustomer()
  );

  const handleSearch = (text: string) => {
    setSearchText(text);
  };

  // Sử dụng searchText để lọc dữ liệu trên phía client
  const filteredData =
    searchText && dataCustomer
      ? dataCustomer.data?.filter((customer) =>
        customer.name.toLowerCase().includes(searchText.toLowerCase())
      )
      : dataCustomer?.data;

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
      title: 'Tên khách hàng',
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
  ];

  return (
    <>
      {dataCustomer && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl text-black'>Quản lý khách hàng</h1>
            </Col>
            <Col span={12}>
              <div className='flex py-2 justify-between items-center gap-3'>
                <Search
                  className='bg-blue-300 rounded-lg'
                  placeholder='Tìm kiếm'
                  onSearch={() => { }}
                  onChange={(e) => handleSearch(e.target.value)}
                />
              </div>
            </Col>
          </Row>
          <Table dataSource={filteredData} columns={columns} />
        </>
      )}
    </>
  );
};
CustomerManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default CustomerManagement;
