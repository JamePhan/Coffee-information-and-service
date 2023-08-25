import { LockOutlined, LogoutOutlined, UserOutlined } from '@ant-design/icons';
import { Avatar, Badge, Dropdown, Layout, Modal, theme as antTheme } from 'antd';
import { AlignJustifyIcon, BellRingIcon } from 'lucide-react';
import Image from 'next/image';
import { useRouter } from 'next/router';
import { useAppDispatch, useAppSelector } from '@/hooks/useRedux';
// import useTrans from '@/hooks/useTrans';
import { logout } from 'src/shared/stores/appSlice';
import IconUser from '@/components/icon/IconUser';
import { APP_SAVE_KEY } from '@/utils/constants';
import { useCookies } from 'react-cookie';

const { Header } = Layout;

const HeaderDashboard = () => {
  const router = useRouter();
  const token = antTheme.useToken();
  const dispatch = useAppDispatch();
  const { user } = useAppSelector(state => state.appSlice);
  // const { changeLanguage, lang, currentcy, trans } = useTrans();
  const [cookies, setCookie, removeCookie] = useCookies([APP_SAVE_KEY.TOKEN_KEY]);

  const onActionClick = () => {
    Modal.confirm({
      title: 'Chắc chắn đăng xuất chứ?',
      okButtonProps: {
        danger: true,
      },
      onOk: () => {
        dispatch(logout());
        removeCookie(APP_SAVE_KEY.TOKEN_KEY, undefined);
        router.push('/login');
      },
    });
  };
  // function RenderFlag() {
  //   switch (lang) {
  //     case 'vi':
  //       return <Image src={'/vietnam.png'} alt='vn' width={24} height={24} />;
  //     case 'en':
  //       return <Image src={'/united-kingdom.png'} alt='vn' width={24} height={24} />;
  //     default:
  //       return <Image src={'/vietnam.png'} alt='vn' width={24} height={24} />;
  //   }
  // }
  return (
    <Header className='layout-page-header bg-2' style={{ backgroundColor: token.token.colorBgContainer }}>
      <div className='lg:hidden'>
        <span
          id='sidebar-trigger'
          className='flex cursor-pointer items-center justify-center rounded-lg p-2 hover:bg-slate-200'
        >
          <AlignJustifyIcon />
        </span>
      </div>
      <div className='flex justify-end w-full h-full pr-4'>
        <div className='flex items-center gap-6'>
          <Dropdown
            placement='bottomRight'
            menu={{
              items: [
                {
                  key: 'vn',
                  label: <div>Vietnamese</div>,
                  onClick: () => {
                    // if (lang !== 'vi') changeLanguage('vi');
                  },
                },
                {
                  key: 'en',
                  label: <div>English </div>,
                  onClick: () => {
                    // if (lang !== 'en') changeLanguage('en');
                  },
                },
              ],
            }}
          >
            <div className='flex flex-col items-center'>
              {/* <RenderFlag /> */}
            </div>
          </Dropdown>
          <Dropdown
            placement='bottomRight'
            menu={{
              items: [
                {
                  key: '1',
                  // label: <div>{trans.common.notification} 1</div>,
                },
                {
                  key: '2',
                  // label: <div>{trans.common.notification} 2 </div>,
                },
              ],
            }}
          >
            <Badge count={2}>
              <BellRingIcon />
            </Badge>
          </Dropdown>
          <Dropdown
            placement='bottomRight'
            menu={{
              items: [
                {
                  key: '1',
                  icon: <UserOutlined />,
                   label: <span>Thông Tin Cá Nhân</span>,
                  onClick: () => router.push('/profile'),
                },
                {
                  key: '2',
                  icon: <LockOutlined />,
                   label: <span>Đổi Mật Khẩu</span>,
                  onClick: () => router.push('/change-password'),
                },
                {
                  key: '3',
                  icon: <LogoutOutlined />,
                   label: <span>Đăng Xuất</span>,
                  onClick: () => onActionClick(),
                },
              ],
            }}
          >
            <div className='flex items-center justify-center gap-1 cursor-pointer'>
              <Avatar>
                <IconUser />
              </Avatar>
              <p className='ipad:hidden text-ellipsis truncate h-max leading-none'>{user && user.name}</p>
            </div>
          </Dropdown>
        </div>
      </div>
    </Header>
  );
};

export default HeaderDashboard;
