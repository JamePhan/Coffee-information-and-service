import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { DeleteOutlined, EditOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, message, Popconfirm, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { IBanner } from 'src/shared/types/banner.type';
import { bannerService } from 'src/shared/services/banner.service';
import FormBanner from './form';
import { useAppSelector } from '@/hooks/useRedux';
import { PreImage } from '@/components/common/PreImage';

type Props = {};

const BannerManagement = ({ }: Props) => {
  const { user } = useAppSelector(state => state.appSlice);
  const [open, setOpen] = useState(false);
  const [action, setAction] = useState<string>('');
  const [rowId, setRowId] = useState<number | undefined>(0);

  const { data: dataBanner, refetch } = useQuery(['listBanner'], () =>
    bannerService.getAllBanner(),
    {
      select: data => {
        const filterData = data.data.filter(item => item.userId === Number(user?.profileId));
        return filterData;
      },
    }
  );

  const deleteMutation = useMutation({
    mutationKey: ['deleteBannerMutation'],
    mutationFn: (bannerId: number) => bannerService.deleteBanner(bannerId),
    onSuccess: () => {
      message.success('Xoá banner thành công');
      refetch();
    },
    onError() {
      message.error('Xoá banner không thành công');
    },
  });

  const columns: ColumnType<IBanner>[] = [
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
      title: 'Ảnh banner',
      dataIndex: 'imageUrl',
      render: (_, record) => (
        <div className='w-[300px] rounded-lg'>
          <PreImage
            width={1980}
            height={150}
            alt={`Image ${record.bannerId}`}
            src={record.imageUrl}
            className='w-full object-cover rounded-lg'
          />
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
              setAction('edit');
              setOpen(true);
              setRowId(record.bannerId);
            }}
          >
            <EditOutlined />
          </div>
          <Popconfirm
            okButtonProps={{ loading: deleteMutation.isLoading }}
            onConfirm={() => {
              deleteMutation.mutate(record.bannerId);
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
      {dataBanner && (
        <div>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl text-black'>Quản lý banner</h1>
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
                <Button
                  onClick={() => {
                    setAction('create');
                    setRowId(0);
                    setOpen(true);
                  }}
                >
                  Tạo mới
                </Button>

              </div>
            </Col>
          </Row>
          <Table dataSource={dataBanner} columns={columns} scroll={{ x: true }} />
          {action === 'create' && rowId === 0 ? (
            <FormBanner
              open={open}
              setOpen={setOpen}
              onFormSubmit={() => {
                refetch();
              }}
              refetch={refetch}
            />
          ) : (
            <FormBanner
              editId={rowId}
              open={open}
              setOpen={setOpen}
              onFormSubmit={() => {
                refetch();
              }}
              refetch={refetch}
            />
          )}
        </div>
      )}
    </>
  );
};

BannerManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default BannerManagement;
