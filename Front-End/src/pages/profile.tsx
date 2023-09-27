import DashboardLayout from '@/components/layout/dashboard/DashboardLayout';
import { useAppSelector } from '@/hooks/useRedux';
import { Row, Col, Form, Input, Button } from 'antd';
import { useState } from 'react';
import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';
import InputUpload from '@/components/common/UploadInput';

const Profile = () => {
  const { user } = useAppSelector(state => state.appSlice);
  const [isEditing, setIsEditing] = useState(false);
  const [formData, setFormData] = useState<IInforUser | undefined>();
  const [avatarUrl, setAvatarUrl] = useState<string | undefined>(user?.avatar);

  const handleAvatarChange = (newAvatarUrl: string) => {
    const updatedAvatarUrl = newAvatarUrl || '';

    setAvatarUrl(updatedAvatarUrl);

    // Kiểm tra nếu formData tồn tại, thì cập nhật avatar trong formData
    if (formData) {
      const updatedFormData = { ...formData, avatar: updatedAvatarUrl };
      setFormData(updatedFormData);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    // Kiểm tra nếu formData tồn tại, thì cập nhật giá trị của trường tương ứng
    if (formData) {
      const { name, value } = e.target;
      const updatedFormData = { ...formData, [name]: value };
      setFormData(updatedFormData);
    }
  };

  const handleUpdateProfile = async () => {
    try {
      if (formData) {
        // Gọi phương thức updateUser của userService để cập nhật thông tin người dùng
        // UserService và response.status cần phải được định nghĩa ở phía bạn
        const response = await userService.updateUser(formData);

        if (response.status === 200) {
          // Xử lý thành công
          setIsEditing(false);
        } else {
          // Xử lý thất bại
        }
      }
    } catch (error) {
      // Xử lý lỗi
    }
  };

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
          autoComplete='off'
        >
          <Form.Item label='Avatar' name='avatar'>
            <InputUpload
              initSrc={avatarUrl || user?.avatar} // Sử dụng user?.avatar như giá trị mặc định
              onChange={handleAvatarChange}
            />
          </Form.Item>
          <Form.Item label='Tên' name='name'>
            <Input
              disabled={!isEditing}
              name='name'
              value={user?.name}
              onChange={handleInputChange}
              placeholder={user?.name} // Sử dụng user?.name như giá trị mặc định
            />
          </Form.Item>
          <Form.Item label='Số điện thoại' name='phone'>
            <Input
              disabled={!isEditing}
              name='phone'
              value={user?.phone}
              onChange={handleInputChange}
              placeholder={user?.phone} // Sử dụng user?.phone như giá trị mặc định
            />
          </Form.Item>
          <Form.Item label='Email' name='email'>
            <Input
              disabled={!isEditing}
              name='email'
              value={user?.email}
              onChange={handleInputChange}
              placeholder={user?.email} // Sử dụng user?.email như giá trị mặc định
            />
          </Form.Item>
          <Form.Item label='Địa chỉ' name='address'>
            <Input
              disabled={!isEditing}
              name='address'
              value={user?.address}
              onChange={handleInputChange}
              placeholder={user?.address} // Sử dụng user?.address như giá trị mặc định
            />
          </Form.Item>

          {/* Hiển thị nút "Cập Nhật Thông Tin" hoặc "Huỷ" tùy thuộc vào trạng thái chỉnh sửa */}
          {isEditing ? (
            <Row gutter={16}>
              <Col span={12}>
                <Form.Item style={{ textAlign: 'center' }}>
                  <Button onClick={() => setIsEditing(false)}>
                    Huỷ
                  </Button>
                </Form.Item>
              </Col>
              <Col span={12}>
                <Form.Item style={{ textAlign: 'center' }}>
                  <Button onClick={handleUpdateProfile}>
                    Cập Nhật Thông Tin
                  </Button>
                </Form.Item>
              </Col>
            </Row>
          ) : (
            <Form.Item style={{ textAlign: 'center' }}>
              <Button onClick={() => setIsEditing(true)}>Chỉnh Sửa Thông Tin</Button>
            </Form.Item>
          )}
        </Form>
      </Col>
    </Row>
  );
};

Profile.getLayout = (children: React.ReactNode) => <DashboardLayout>{children}</DashboardLayout>;
export default Profile;
