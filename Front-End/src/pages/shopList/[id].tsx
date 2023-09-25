import React from 'react';
import Head from 'next/head';
import { IInforUser } from 'src/shared/types/user.type';
import { PreImage } from '@/components/common/PreImage';


const containerStyle = {
    width: '100%',
    height: '400px', // Điều chỉnh kích thước bản đồ tùy ý
};

interface Props {
    shopListData: IInforUser;
}

const UsersDetail = ({ shopListData }: Props) => {
    if (!shopListData) return <></>;

    return (
        <>
            <Head>
                <title>Thông tin quán cà phê</title>
                <meta name='description' content='Thông tin quán cà phê' />
                <meta name='keywords' content='Coffee Shop' />
            </Head>
            {shopListData && (
                <div className='w-full h-full flex flex-col justify-start items-start dark:bg-[#1B1D35] bg-slate-100 pb-12'>
                    <div className='relative w-full flex justify-between items-center mx-auto'>
                        <div className='relative w-full flex-shrink-0 snap-start'>
                            <PreImage
                                src={shopListData.avatar}
                                height={400}
                                width={1980}
                                layer={false}
                                alt={'Banner News'}
                                className='w-full object-cover'
                            />
                        </div>
                    </div>
                    <div className='w-full p-5 flex flex-col justify-center items-start gap-3'>
                        <div className='w-full flex justify-start items-center gap-5'>
                            <h3 className='text-3xl'>{shopListData.coffeeShopName}</h3>
                            <p className='text-slate-400'>Tác giả: {shopListData.coffeeShopName}</p>
                        </div>
                        <div className='mt-10 w-full h-full'>
                            <p>{shopListData.email}</p>
                            <div className='mt-5'>
                                {/* Hiển thị Google Map */}

                            </div>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
};

export async function getStaticProps({ params }: any) {
    const { id } = params;
    const res = await fetch(`${process.env.NEXT_PUBLIC_API}/User/Detail/${id}`);
    const shopListData = await res.json();

    return {
        props: {
            shopListData,
        },
    };
}

export async function getStaticPaths() {
    return {
        paths: [],
        fallback: false,
    };
}

export default UsersDetail;
