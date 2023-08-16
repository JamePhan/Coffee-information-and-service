import BlankLayout from '@/components/layout/BlankLayout';
import { useAppDispatch } from '@/hooks/useRedux';
import { APP_SAVE_KEY } from '@/utils/constants';
import { useMutation } from '@tanstack/react-query';
import { Button, Card, Checkbox, Col, Form, Input, message, Row } from 'antd';
import { getCookie, setCookie } from 'cookies-next';
import { useRouter } from 'next/router';
import { authService } from 'src/shared/services/authentication.service';
import jwt_decode from 'jwt-decode';
import { IUserRegister } from 'src/shared/types/user.type';

const Register = () => {
  const dispatch = useAppDispatch();
  const router = useRouter();
  const registerMutation = useMutation({
    mutationFn: (body: IUserRegister) => authService.register(body),
    onSuccess(data, _variables, _context) {
      const res = data.data;
      if (!res) return;
      message.error('Đăng ký không thành công');
    },
    onError(error, variables, context) {
      message.error('Đăng ký không thành công');
    },
  });

  function handleRegister(value: any) {
    registerMutation.mutate(value);
  }

  return (
    <>
      <div className='relative h-full flex-col items-center justify-center md:grid lg:max-w-none lg:grid-cols-2 lg:px-0'>
        <div className='relative md:h-full flex-col bg-muted p-10 text-white dark:border-r lg:flex'>
          <div
            className='absolute inset-0 register-background'
            style={{
              backgroundImage: `url("/bg-login.jpg")`,
              backgroundOrigin: 'initial',
              backgroundPosition: 'center',
              backgroundRepeat: 'no-repeat',
              backgroundSize: 'cover',
              backdropFilter: 'blur(3px)',
            }}
          />
          <div className={`bg-black absolute top-0 left-0 w-full h-full opacity-50`}></div>

          <div className='relative z-20 h-10 flex justify-start text-lg font-medium'>
            <b>COFFEE INFORMATION & SERVICE</b>
          </div>
          <div className='relative z-20 mt-auto'>
            <h1 className='text-4xl font-semibold tracking-tight'>
              <div className='text-yellow-400'>COFFEE</div>
              <div>INFORMATION & SERVICE</div>
            </h1>
            <p className='mt-4 text-lg'>
              Lorem ipsum dolor sit amet consectetur adipisicing elit. Omnis nostrum architecto neque. Enim voluptas
              recusandae necessitatibus officia vero porro alias facere non atque aut, adipisci dolores sit libero
              asperiores explicabo.
            </p>
          </div>
          <div className='relative z-20 mt-auto'>
            <p className='text-lg'>Copyright &copy; Coffee Information&Service 2023</p>
          </div>
        </div>
        <div className='lg:p-8'>
          <div className='mx-auto flex w-full flex-col justify-center space-y-6 sm:w-[450px]'>
            <div className='font-bold text-3xl text-center w-full text-black'>Đăng ký</div>
            <Card className='shadow' size='default'>
              <Form
                name='basic'
                style={{ maxWidth: 700 }}
                initialValues={{ remember: true }}
                onFinish={handleRegister}
                autoComplete='off'
              >
                <Form.Item
                  label='Username'
                  name='username'
                  rules={[{ required: true, message: 'Please input your username!' }]}
                >
                  <Input size='large' />
                </Form.Item>

                <Form.Item
                  label='Password'
                  name='password'
                  rules={[{ required: true, message: 'Please input your password!' }]}
                >
                  <Input.Password size='large' />
                </Form.Item>

                <Row gutter={16} justify={'space-between'}>
                  <Col span={12}>
                    <Form.Item name='remember' valuePropName='checked'>
                      <Checkbox>Remember me</Checkbox>
                    </Form.Item>
                  </Col>
                  <Col className='text-right' span={12}>
                    <p onClick={() => router.push('/login')} className='m-0 p-0'>
                      Login
                    </p>
                  </Col>
                </Row>

                <Form.Item className='w-full'>
                  <Button className='w-full' htmlType='submit' loading={registerMutation.isLoading}>
                    Submit
                  </Button>
                </Form.Item>
              </Form>
            </Card>
          </div>
        </div>
      </div>
    </>
  );
};

Register.getLayout = (children: React.ReactNode) => <BlankLayout>{children}</BlankLayout>;
export default Register;
