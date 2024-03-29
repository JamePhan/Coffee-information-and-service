import Dashboard from '@/components/layout/dashboard/DashboardLayout';
import { DeleteOutlined, EditOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Col, message, Popconfirm, Row, Space, Table } from 'antd';
import { ColumnType } from 'antd/lib/table';
import { useEffect, useState } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import FormNews from './form';
import { newsService } from 'src/shared/services/news.service';
import { INews } from 'src/shared/types/news.type';
import { useAppSelector } from '@/hooks/useRedux';
import { PreImage } from '@/components/common/PreImage';

type Props = {};

const NewsManagement = ({ }: Props) => {
  const { user } = useAppSelector(state => state.appSlice);
  const [open, setOpen] = useState(false);
  const [action, setAtion] = useState<string>('');
  const [rowId, setRowId] = useState<number>();
  const { data: dataNews, refetch } = useQuery(['listNews'], () => newsService.getAllNews(), {
    select: data => {

      const filterData = data.data.filter(item => item.coffeeShopName === user?.name);
      return filterData;
    },
  });

  const deleteMutation = useMutation({
    mutationKey: ['deleteNewsMutation'],
    mutationFn: (NewsId: number) => newsService.deleteNews(NewsId),
    onSuccess: () => {
      message.success('Xoá thành công');
      refetch();
    },
    onError() {
      message.error('Xoá không thành công');
    },
  });

  const columns: ColumnType<INews>[] = [
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
      title: 'Tiêu đề',
      dataIndex: 'title',
      key: 'title',
    },
    {
      title: 'Tên địa điểm',
      dataIndex: 'imageUrl',
      render: (_, record) => (
        <div className='w-[300px] rounded-lg'>
          <PreImage
            width={1980}
            height={150}
            alt={record.title}
            src={record.imageUrl}
            className='w-full object-cover rounded-lg'
          />
        </div>
      ),
    },
    {
      title: 'Mô tả',
      dataIndex: 'description',
      key: 'description',
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
              setRowId(record.newsId);
            }}
          >
            <EditOutlined />
          </div>
          <Popconfirm
            okButtonProps={{ loading: deleteMutation.isLoading }}
            onConfirm={() => {
              deleteMutation.mutate(record.newsId);
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
      {dataNews && (
        <>
          <Row className='mb-12' justify={'space-between'} align='middle' gutter={16}>
            <Col span={12}>
              <h1 className='font-bold text-2xl  text-black'>Quản lý tin tức</h1>
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

          <Table dataSource={dataNews} columns={columns} scroll={{ x: true }} />
          {action === 'create' && !rowId ? (
            <FormNews refetch={refetch} open={open} setOpen={setOpen} coffeeShopName={user?.name} />
          ) : (
            <FormNews refetch={refetch} editId={rowId} open={open} coffeeShopName={user?.name} setOpen={setOpen} />
          )}
        </>
      )}
    </>
  );
};
NewsManagement.getLayout = (children: React.ReactNode) => <Dashboard>{children}</Dashboard>;
export default NewsManagement;
