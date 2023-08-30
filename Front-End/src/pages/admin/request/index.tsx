import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { CheckOutlined, ReloadOutlined, CloseOutlined } from '@ant-design/icons';
import { Button, Col, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useQuery, useMutation } from '@tanstack/react-query'; // Import useMutation
import { requestService } from 'src/shared/services/request.service';
import { userService } from 'src/shared/services/user.service'; // Import userService
import { IRequest } from 'src/shared/types/request.type';

type Props = {};

const RequestManagement = ({}: Props) => {
  const [open, setOpen] = useState(false);
  const [action, setAction] = useState<string>('');
  const [rowId, setRowId] = useState<number>();

  const { data: dataRequest, refetch } = useQuery(['listRequest'], () => requestService.getAllRequest());

  const acceptRequestMutation = useMutation(
    (requestId: number) => requestService.acceptRequest(requestId), // Đổi tên hàm và truyền requestId
    {
      onMutate() {
        // Optimistically update the request list by removing the accepted request
        const updatedData = dataRequest?.data?.filter(request => request.customerId !== rowId);
        refetch(updatedData); // Refetch with the updated data
      },
      onSuccess(_data, _variables, _context) {
        message.success('Đã chấp nhận yêu cầu!');
        refetch(); // Làm mới danh sách yêu cầu (nếu cần)
        // Sử dụng userService để thay đổi vai trò người dùng từ "customer" thành "user"
        userService.changeUserRole(rowId, 'user')
          .then(() => {
            message.success('Đã chuyển vai trò thành user');
          })
          .catch((error) => {
            console.error('Lỗi khi chuyển vai trò', error);
            message.error('Lỗi khi chuyển vai trò');
          });
      },
      onError(error, variables, context) {
        message.error('Đã xảy ra lỗi khi gửi yêu cầu');
      },
    }
  );

  const declineRequestMutation = useMutation(
    (requestId: number) => requestService.declineRequest(requestId), // Đổi tên hàm và truyền requestId
    {
      onMutate() {
        // Optimistically update the request list by removing the declined request
        const updatedData = dataRequest?.data?.filter(request => request.customerId !== rowId);
        refetch(updatedData); // Refetch with the updated data
      },
      onSuccess(_data, _variables, _context) {
        message.success('Đã từ chối yêu cầu!');
        refetch(); // Làm mới danh sách yêu cầu (nếu cần)
      },
      onError(error, variables, context) {
        message.error('Đã xảy ra lỗi khi gửi yêu cầu');
      },
    }
  );

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
          <div className='cursor-pointer'>
            <Button
              onClick={() => {
                setRowId(record.customerId);
                acceptRequestMutation.mutate(record.requestId); // Khi chấp nhận, gọi mutation acceptRequest
              }}
              icon={<CheckOutlined />}
            >
              Chấp nhận
            </Button>
          </div>
          <div className='cursor-pointer'>
            <Button
              onClick={() => {
                setRowId(record.customerId);
                declineRequestMutation.mutate(record.requestId); // Khi từ chối, gọi mutation declineRequest
              }}
              icon={<CloseOutlined />}
            >
              Từ chối
            </Button>
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
