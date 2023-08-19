import DashboardLayout from '@/components/layout/dashboard/DashboardLayout';
import { useAppSelector } from '@/hooks/useRedux';
import { Row, Col, Form, Input } from 'antd';

const Profile = () => {
  const { user } = useAppSelector(state => state.appSlice);
  return (
    <Row className='h-screen w-full' gutter={[16, 16]}>
      <Col className='m-0 h-full w-full p-0' span={12}>
        <div
          className=' w-full h-screen mobile:h-full'
          style={{
            backgroundImage: `url("/bg-login.jpg")`,
            backgroundOrigin: 'initial',
            backgroundPosition: 'center',
            backgroundRepeat: 'no-repeat',
            backgroundSize: 'cover',
            opacity: 1,
          }}
        ></div>
      </Col>
      <Col span={12}>
        <Form
          name='basic'
          layout='vertical'
          style={{ maxWidth: 600 }}
          initialValues={{ remember: true }}
          autoComplete='off'
        >
          <Form.Item label='Tên' name='name'>
            <Input disabled placeholder={user?.name} />
          </Form.Item>
          <Row gutter={16}>
            <Col span={12}>
              <Form.Item label='Số điện thoại' name='phone'>
                <Input disabled placeholder={user?.phone} />
              </Form.Item>
            </Col>
            <Col span={12}>
              <Form.Item label='Email' name='email'>
                <Input disabled placeholder={user?.email} />
              </Form.Item>
            </Col>
          </Row>
          <Form.Item label='Địa chỉ' name='address'>
            <Input disabled placeholder={user?.address} />
          </Form.Item>
        </Form>
      </Col>
    </Row>
  );
};

Profile.getLayout = (children: React.ReactNode) => <DashboardLayout>{children}</DashboardLayout>;
export default Profile;
