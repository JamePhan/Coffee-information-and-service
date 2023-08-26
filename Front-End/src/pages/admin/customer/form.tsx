import InputUpload from '@/components/common/UploadInput';
import { useMutation, useQuery } from '@tanstack/react-query';
import { Button, Form, Input, message, Modal, Row, Col } from 'antd';
import { useForm } from 'antd/lib/form/Form';
import { useEffect } from 'react';
import { userService } from 'src/shared/services/user.service';
import { IInforUser } from 'src/shared/types/user.type';

interface Props {
  editId?: number;
  open: any;
  setOpen: any;
  refetch: any;
}
const FormUser = ({ editId, open, setOpen, refetch }: Props) => {
  const [form] = useForm();
  const isEditIdValidNumber = typeof editId === 'number';
  const updateMutation = useMutation({
    mutationFn: (body: IInforUser) => userService.updateUser(body),
    onSuccess(data, _variables, _context) {
      const res = data.data;
      if (!res) return;
      message.success('Cập nhật thành công');
      setOpen(false);
      refetch();
    },
    onError(error, variables, context) {
      message.error('Cập nhật không thành công');
    },
  });
  function handleCreate(value: any) {
    if (editId) {
        const formUpdate = {
            userId: editId,
            ...value
        }
      updateMutation.mutate(formUpdate);
    }
  }
  const { data } = useQuery(['User'], () => userService.getUserById(editId as number), {
    enabled: isEditIdValidNumber,
  });
  useEffect(() => {
    if (editId && data) {
      form.setFieldsValue(data.data);
    }
  }, [data]);
  return (
    <Modal title={editId ? `Chỉnh sửa khách hàng` : 'Tạo khách hàng mới'} centered open={open} width={1000} footer={false}>
      <Form
        form={form}
        name='basic'
        initialValues={{ remember: true }}
        onFinish={handleCreate}
        autoComplete='off'
        layout='vertical'
      >
        <Form.Item label='Tên khách hàng' name='coffeeShopName' rules={[{ required: true, message: 'Vui lòng nhập khách hàng' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Ảnh' name='avatar' rules={[{ required: true, message: 'Vui lòng nhập ảnh' }]}>
          <InputUpload initSrc={data?.data.avatar} />
        </Form.Item>

        <Form.Item label='Số điện thoại' name='phone' rules={[{ required: true, message: 'Vui lòng nhập số điện thoại' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Địa chỉ' name='address' rules={[{ required: true, message: 'Vui lòng nhập địa chỉ' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Email' name='email' rules={[{ required: true, message: 'Vui lòng nhập email' }]}>
          <Input />
        </Form.Item>
        
        <Row justify={'center'} align={'middle'} gutter={16}>
          <Col>
            <Form.Item style={{ textAlign: 'center' }}>
              <Button onClick={() => setOpen(false)} htmlType='button'>
                Huỷ bỏ
              </Button>
            </Form.Item>
          </Col>
          <Col>
            <Form.Item style={{ textAlign: 'center' }}>
              <Button htmlType='submit'>{editId ? 'Chỉnh sửa' : 'Tạo mới'}</Button>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </Modal>
  );
};

export default FormUser;
