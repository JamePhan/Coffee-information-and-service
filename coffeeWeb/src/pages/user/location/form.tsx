import { useMutation, useQuery } from '@tanstack/react-query';
import { Button, Form, Input, message, Modal, Row, Col, DatePicker } from 'antd';
import { useForm } from 'antd/lib/form/Form';
import { useEffect } from 'react';
import { locationService } from 'src/shared/services/location.service';
import { ILocation } from 'src/shared/types/location.type';

interface Props {
  editId?: number;
  open: any;
  setOpen: any;
  refetch: any;
}
const FormLocation = ({ editId, open, setOpen, refetch }: Props) => {
  const [form] = useForm();
  const isEditIdValidNumber = typeof editId === 'number';
  const createMutation = useMutation({
    mutationFn: (body: ILocation) => locationService.newLocation(body),
    onSuccess(data, _variables, _context) {
      const res = data.data;
      if (!res) return;
      message.success('Tạo thành công');
      setOpen(false);
      refetch();
    },
    onError(error, variables, context) {
      message.error('Tạo không thành công');
    },
  });
  const updateMutation = useMutation({
    mutationFn: (body: ILocation) => locationService.updateLocation(editId as number, body),
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
      updateMutation.mutate(value);
    } else {
      createMutation.mutate(value);
    }
  }
  const { data } = useQuery(['Location'], () => locationService.getLocationById(editId as number), {
    enabled: isEditIdValidNumber,
  });
  useEffect(() => {
    if (editId && data) {
      form.setFieldsValue(data.data);
    }
  }, [data]);
  return (
    <Modal title={editId ? `Chỉnh sửa địa điểm` : 'Tạo địa điểm mới'} centered open={open} width={1000} footer={false}>
      <Form
        form={form}
        name='basic'
        initialValues={{ remember: true }}
        onFinish={handleCreate}
        autoComplete='off'
        layout='vertical'
      >
        <Form.Item label='Tên' name='plusCode' rules={[{ required: true, message: 'Vui lòng nhập' }]}>
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

export default FormLocation;