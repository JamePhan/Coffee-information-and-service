import Head from 'next/head';
import LayoutWebsite from 'src/shared/components/layout/LayoutWebsite';
import dynamic from 'next/dynamic';

const ScrollRevealWrapper = dynamic(() => import('src/shared/components/common/ScrollRevealWrapper'), { ssr: false });

export function FollowingPage() {
  return (
    <>
      <Head>
        <title>Yêu thích Coffee Shop</title>
        <meta name='description' content='Sự kiện Coffee Shop' />
        <meta name='keywords' content='Coffee Shop' />
      </Head>
      <ScrollRevealWrapper>
        <></>
      </ScrollRevealWrapper>
    </>
  );
}
FollowingPage.getLayout = (children: React.ReactNode) => <LayoutWebsite>{children}</LayoutWebsite>;
export default FollowingPage;
