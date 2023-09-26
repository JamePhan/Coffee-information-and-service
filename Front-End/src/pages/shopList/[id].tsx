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
                    <div className='w-full p-5 flex flex-col md:flex-row md:justify-center md:items-center gap-5'>
                        <div className='w-full md:w-1/2 flex justify-center items-center'>
                            <img
                                src={shopListData.avatar}
                                height={100}
                                width={100}
                                alt={'Avatar'}
                                className='w-72 h-72 rounded-full object-cover mx-auto'
                            />
                        </div>

                        <div className='w-full md:w-1/2'>
                            <h3 className='text-5xl mt-5'>{shopListData.coffeeShopName}</h3>
                            <br></br><br></br>
                            <h4 className='text-xl font-semibold'>Thông tin chi tiết:</h4>
                            <p className='text-white-600'>Địa chỉ: {shopListData.address}</p>
                            <p className='text-white-600'>Số điện thoại: {shopListData.phone}</p>
                            <p className='text-white-600'>User ID: {shopListData.userId}</p>
                            <p className='text-white-600'>Email:{shopListData.email}</p>

                        </div>
                    </div>


                    <div className='mt-10 w-full h-full'>
                        {/* Hiển thị Google Map */}
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3834.2199884156345!2d105.7938072155048!3d21.024424001245553!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3135ab06f6633685%3A0xfd1de3ba2e978c28!2zVGhlIENvZmZlZSBIb3VzZQ!5e0!3m2!1sen!2s!4v1630918443940!5m2!1sen!2s"
                            width="100%"
                            height="400"
                            style={{ border: 0 }}
                            // allowFullScreen=""
                            loading="lazy"
                        ></iframe>
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
