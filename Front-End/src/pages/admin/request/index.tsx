import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { CheckOutlined, ReloadOutlined, CloseOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { requestService } from 'src/shared/services/request.service';
import { IRequest } from 'src/shared/types/request.type';

type Props = {};

const RequestManagement = ({}: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataRequest, refetch } = useQuery(['listRequest'], () => requestService.getAllRequest());
  const columns: ColumnType<IRequest>[] = [
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
            //   setAtion('edit');
            //   setOpen(true);
              setRowId(record.customerId);
            }}
          >
            <CheckOutlined />
          </div>
          <div
            className='cursor-pointer'
            onClick={() => {
            //   setAtion('edit');
            //   setOpen(true);
              setRowId(record.customerId);
            }}
          >
            <CloseOutlined />
          </div>
        </Space>
      ),
    },
  ];

  return (
    <>
      {dataRequest && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl text-black'>Quản lý phê duyệt</h1>
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
          <Table dataSource={dataRequest.data} columns={columns} />
        </>
      )}
    </>
  );
};
RequestManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default RequestManagement;
