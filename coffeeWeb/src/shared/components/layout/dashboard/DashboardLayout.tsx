import React from 'react';
import {
  AppstoreOutlined,
  BookOutlined,
  InsertRowLeftOutlined,
  UsergroupAddOutlined,
  UserOutlined,
} from '@ant-design/icons';
import { MenuProps } from 'antd';
import { Layout, Menu } from 'antd';
import { useRouter } from 'next/router';
import HeaderDashboard from './HeaderDashboard';
import { PreImage } from '@/components/common/PreImage';

const { Content, Footer, Sider } = Layout;
type MenuItem = Required<MenuProps>['items'][number];

function getItem(label: React.ReactNode, key: React.Key, icon?: React.ReactNode, children?: MenuItem[]): MenuItem {
  return {
    key,
    icon,
    children,
    label,
  } as MenuItem;
}
interface Props {
  children: React.ReactNode;
}
const items: MenuItem[] = [
  getItem('Sự kiện', '/user/event', <UserOutlined />),
  getItem('Tin tức', '/user/news', <AppstoreOutlined />),
  getItem('Địa điểm', '/user/location', <UsergroupAddOutlined />),
  getItem('Dịch vụ', '/user/serviceCoffee', <InsertRowLeftOutlined />),
  getItem('Khách hàng thân thiết', '/user/customer-following', <BookOutlined />),
];

const DashboardLayout = ({ children }: Props) => {
  const router = useRouter();
  const currentPath = router.pathname;

  return (
    <>
      <Layout style={{ minHeight: '100vh' }}>
        <Sider style={{ backgroundColor: '#fff' }}>
          <div className='flex justify-center items-center'>
            <PreImage className="cursor-pointer" height={100} width={100} src={'/logo.png'} alt={'Logo'} layer={false} />
          </div>
          <Menu
            className='items-start justify-start'
            onClick={item => router.push(item.key)}
            theme='light'
            defaultSelectedKeys={[`${currentPath.split('/').splice(0, 3).join('/')}` || `${currentPath}`]}
            mode='inline'
            items={items}
          ></Menu>
        </Sider>
        <Layout>
          <HeaderDashboard />
          <Content className='m-6 min-h-screen rounded-lg bg-white mobile:m-4'>
            <div className='p-2'>{children}</div>
          </Content>
          <Footer style={{ textAlign: 'center' }}>Copyright ©2023 Created by ThuanVuVan</Footer>
        </Layout>
      </Layout>
    </>
  );
};

export default DashboardLayout;
