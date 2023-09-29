import LayoutWebsite from '@/components/layout/LayoutWebsite';
import { Typography, Button } from 'antd';

const { Title, Paragraph, Text } = Typography;

const AboutUs = () => {
  return (
    <>
      <div className='container mx-auto text-center py-12'>
        <h1 className='text-4xl font-bold text-coffee-primary mb-4'>Welcome to Coffee Information & Service</h1>
        <h3 className='text-white-800'>
          We are passionate about coffee and committed to providing you with the latest information, tips, and
          resources to enhance your coffee experience.
        </h3>

      </div>

      <div className='bg-coffee-secondary py-12'>
        <div className='container mx-auto text-center'>
          <h2 className='text-2xl font-semibold text-white mb-8'>Our Services</h2>
          <div className='grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-8'>
            {/* Service 1 */}
            <div className='bg-white p-6 rounded-lg shadow-lg px-4 py-6 transform transition duration-500 hover:scale-110'>
              <Title level={4}>Coffee Tasting</Title>
              <Text className='text-gray-800'>
                Join us for a coffee tasting journey where you can explore the rich flavors and aromas of different
                coffee beans.
              </Text>
            </div>

            {/* Service 2 */}
            <div className='bg-white p-6 rounded-lg shadow-lg px-4 py-6 transform transition duration-500 hover:scale-110'>
              <Title level={4}>Brewing Workshops</Title>
              <Text className='text-gray-800'>
                Learn the art of coffee brewing from our experts. Discover various brewing methods and perfect your
                skills.
              </Text>
            </div>

            {/* Service 3 */}
            <div className='bg-white p-6 rounded-lg shadow-lg px-4 py-6 transform transition duration-500 hover:scale-110'>
              <Title level={4}>Coffee News</Title>
              <Text className='text-gray-800'>
                Stay updated with the latest coffee news and trends. Get insights into the coffee industry and its
                developments.
              </Text>
            </div>
          </div>
        </div>
      </div>



      <div className='py-12'>
        <div className='container mx-auto'>
          <h2 className='text-2xl font-semibold text-white mb-4'>Visit Our Coffee Shop</h2>
          <div className='map-current w-full h-[500px]'>
            <iframe
              className='w-full h-full'
              src='https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3918.654985449266!2d106.83037941436093!3d10.837693192279962!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x317521ba9415b293%3A0xb6d3b7f0c8d2a78c!2sVinhome%20Grand%20Park%20Homestay%20House%20(Phong%20Nh%C3%A3)!5e0!3m2!1svi!2s!4v1676203944217!5m2!1svi!2s'
              allowFullScreen={true}
              loading='lazy'
              referrerPolicy='no-referrer-when-downgrade'
            ></iframe>
          </div>
        </div>
      </div>


      <div className='py-8 text-center'>
        <a
          href='https://www.google.com/maps/place/Vinhome+Grand+Park+Homestay+House+(Phong+Nh%C3%A3)/@10.552277,106.94169,7z/data=!4m9!3m8!1s0x317521ba9415b293:0xb6d3b7f0c8d2a78c!5m2!4m1!1i2!8m2!3d10.8376932!4d106.8325681!16s%2Fg%2F11sr7vbcxx?hl=vi'
          className='text-blue-500 hover:underline'
        >
          View on Google Maps
        </a>
      </div>
    </>
  );
};

AboutUs.getLayout = (children: React.ReactNode) => <LayoutWebsite>{children}</LayoutWebsite>;

export default AboutUs;
