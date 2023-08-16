import { PreImage } from '@/components/common/PreImage';
import { newsData } from '@/mocks/news';
import { APP_SAVE_KEY } from '@/utils/constants';
import { useQuery } from '@tanstack/react-query';
import { getCookie } from 'cookies-next';
import Head from 'next/head';
import { useRouter } from 'next/router';
import { newsService } from 'src/shared/services/news.service';
import { INews } from 'src/shared/types/news.type';

interface Props {
  newsData: INews;
}
const NewsDetail = ({newsData}: Props) => {
  if(!newsData) return <></>
  return (
    <>
      <Head>
        <title>Tin tức Coffee Shop</title>
        <meta name='description' content='Tin tức Coffee Shop' />
        <meta name='keywords' content='Coffee Shop' />
      </Head>
      {newsData && (
        <div className='w-full h-full flex flex-col justify-start items-start  dark:bg-[#1B1D35] bg-slate-100 pb-12'>
          <div className='relative w-full flex justify-between items-center mx-auto'>
            <div className='relative w-full flex-shrink-0 snap-start'>
              <PreImage
                src={newsData.imageUrl}
                height={400}
                width={1980}
                layer={false}
                alt={'Banner News'}
                className='w-full object-cover'
              />
            </div>
          </div>
          <div className=' w-full p-5 flex flex-col justify-center items-start gap-3'>
            <div className='w-full flex justify-start items-center gap-5'>
              <h3 className='text-3xl'>{newsData.title}</h3>
              <p className='text-slate-400'>Tác giả: {newsData.coffeeShopName}</p>
            </div>
            <div className='mt-10 w-full h-full'>
              <p>{newsData.description}</p>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export async function getStaticProps({ params }: any) {
  const { id } = params;
  const res = await fetch(`${process.env.NEXT_PUBLIC_API}/News/Detail/${id}`)
  const newsData = await res.json();

  return {
    props: {
      newsData,
    },
  };
}
export async function getStaticPaths() {
  return {
    paths: [],
    fallback: false
  };
}

export default NewsDetail;
