import { theme } from 'antd';
import React, { useState, useEffect } from 'react';
import Head from 'next/head';
import LayoutWebsite from 'src/shared/components/layout/LayoutWebsite';
import { userService } from 'src/shared/services/user.service'; 
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { followingService } from 'src/shared/services/following.service'; 
import { IFollowing, IFollowingAdd } from 'src/shared/types/following.type'; 
import { useAppSelector } from '@/hooks/useRedux';
import { APP_SAVE_KEY } from '@/utils/constants';
import { getCookie } from 'cookies-next';

import dynamic from 'next/dynamic';
import Brands from '@/components/home/brands';

const ScrollRevealWrapper = dynamic(() => import('src/shared/components/common/ScrollRevealWrapper'), { ssr: false });
export function ShopListPage() {

    const [role, setRole] = useState<string>('');
    const { user } = useAppSelector(state => state.appSlice);
    const [userType, setUserType] = useState<string>();

    const { data: userData, refetch } = useQuery(['listUsers'], () => userService.getAllUser());

    useEffect(() => {
        const role = getCookie(APP_SAVE_KEY.ROLE);
        setRole(role as string);
      }, []);
    
    
      useEffect(() => {
        if (role === 'Customer') {
          setUserType('Customer');
        } else if (role === 'User') {
          setUserType('User');
        }
      }, [role]);
      return (
        <>
          <Head>
            <title>Yêu thích Coffee Shop</title>
            <meta name='description' content='Sự kiện Coffee Shop' />
            <meta name='keywords' content='Coffee Shop' />
          </Head>
          {userData && (
            <ScrollRevealWrapper>
              <Brands brandsData={userData.data}></Brands>
            </ScrollRevealWrapper>
          )}
        </>
      );
}

export default ShopListPage;


