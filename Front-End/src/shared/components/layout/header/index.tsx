import Link from 'next/link';
import { motion } from 'framer-motion';
import { useEffect, useState } from 'react';
import { Menu } from 'lucide-react';
import { useRouter } from 'next/router';

import { fakeMenu, IMenu } from '@/mocks/menu/website';
import { PreImage } from '@/components/common/PreImage';
import ThemeModeToggle from '@/components/common/ToggleThemeMode';
import LanguageSwitch from '@/components/common/LanguageSwitch';
import { Sheet, SheetContent, SheetTrigger } from '@/components/common/ui/sheet';

import { HambugerMenu } from './HambugerMenu';
import { Button } from '@/components/common/ui/button';
import { getCookie } from 'cookies-next';
import { APP_SAVE_KEY } from '@/utils/constants';
import { Modal } from 'antd';
import { useAppDispatch, useAppSelector } from '@/hooks/useRedux';
import { logout } from 'src/shared/stores/appSlice';
import { useCookies } from 'react-cookie';

interface Props{
  isLogin: any
}
const Header = ({isLogin}: Props) => {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector(state => state.appSlice);
  const [_, __, removeCookie] = useCookies([APP_SAVE_KEY.TOKEN_KEY]);
  const [menuData, setMenuData] = useState<IMenu[]>();
  const [isLogout, setIsLogout] = useState(false);
  const router = useRouter();
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
  useEffect(() => {
    setIsLogout(true);
    const menuCurrent =
      isLogin && user?.role === 'Customer'
        ? fakeMenu.slice(0, 6)
        : user?.role === 'User'
        ? fakeMenu
        : fakeMenu.slice(0, 4);
    return setMenuData(menuCurrent);
  }, [isLogin, user]);

  const [isScrolled, setIsScrolled] = useState(false);
  useEffect(() => {
    const handleScroll = () => {
      const scrollTop = window.pageYOffset || document.documentElement.scrollTop;

      if (scrollTop > 150 && !isScrolled) {
        setIsScrolled(true);
      } else if (scrollTop === 0 && isScrolled) {
        setIsScrolled(false);
      }
    };

    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, [isScrolled]);

  return (
    <motion.section
      initial={{ height: '100px' }}
      animate={{ height: isScrolled ? '80px' : '100px' }}
      transition={{ duration: 0.3 }}
      className={`bg-white w-full top-0 z-50 flex justify-between items-center gap-5 px-5 md:px-10 transition ${
        isScrolled
          ? 'sticky light:text-black border-b-[1px] border-opacity-50 border-black-300 inset-0 bg-opacity-10 backdrop-filter backdrop-blur duration-500 ease-in-out light:bg-[#141523]'
          : 'sticky dark:bg-[#141523] bg-opacity-100 duration-500 ease-in-out'
      }`}
    >
      <div className='flex justify-around items-center'>
        <PreImage
          onClick={() => router.push('/')}
          className='cursor-pointer'
          height={100}
          width={100}
          src={'/Logo2.png'}
          alt={'Logo'}
          layer={false}
        />
        <ul className='justify-center items-center gap-10 hidden lg:flex ml-10 dark:text-white'>
          {menuData &&
            menuData.map((item, idx) => (
              <Link href={item.path} key={idx}>
                <li>{item.label}</li>
              </Link>
            ))}
        </ul>
      </div>
      <div className='absolute right-5 flex justify-center items-center gap-2'>
        <div
          id='dropdown'
          className='z-10 hidden bg-white divide-y divide-gray-100 rounded-lg shadow w-44 dark:bg-gray-700'
        >
          <ul className='py-2 text-sm text-gray-700 dark:text-gray-200' aria-labelledby='dropdownDefaultButton'>
            <li>
              <a href='#' className='block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white'>
                Dashboard
              </a>
            </li>
            <li>
              <a href='#' className='block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white'>
                Settings
              </a>
            </li>
            <li>
              <a href='#' className='block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white'>
                Earnings
              </a>
            </li>
            <li>
              <a href='#' className='block px-4 py-2 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white'>
                Sign out
              </a>
            </li>
          </ul>
        </div>
        <div className='flex gap-8 items-center'>
          <div className='lg:hidden'>
            <Sheet>
              <SheetTrigger asChild>
                <div>
                  <Menu />
                </div>
              </SheetTrigger>
              <SheetContent className='w-[200px]' side={'left'}>
                <div>
                  {fakeMenu.map((item, idx) => (
                    <Link href={item.path} key={idx}>
                      <li>{item.label}</li>
                    </Link>
                  ))}
                </div>
              </SheetContent>
            </Sheet>
          </div>

          <ThemeModeToggle />
          <LanguageSwitch />
          {isLogin ? (
            <button
              onClick={() => onActionClick()}
              className='dark:text-white font-bold py-2 px-4 rounded cursor-pointer hidden lg:block'
            >
              Đăng xuất
            </button>
          ) : (
            <button
              onClick={() => {
                router.push('/login');
              }}
              className='dark:text-white font-bold py-2 px-4 rounded cursor-pointer hidden lg:block'
            >
              Đăng nhập
            </button>
          )}
        </div>
      </div>
    </motion.section>
  );
};

export default Header;
