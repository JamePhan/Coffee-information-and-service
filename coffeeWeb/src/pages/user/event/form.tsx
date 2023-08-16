import { useMutation, useQuery } from '@tanstack/react-query';
import { Button, Form, Input, message, Modal, Row, Col, DatePicker } from 'antd';
import { useForm } from 'antd/lib/form/Form';
import { useEffect } from 'react';
import { eventService } from 'src/shared/services/event.service';
import { IEvent } from 'src/shared/types/event.type';

interface Props {
  editId?: number;
  open: any;
  setOpen: any;
  refetch: any;
}
const FormEvent = ({ editId, open, setOpen, refetch }: Props) => {
  const [form] = useForm();
  const isEditIdValidNumber = typeof editId === 'number';
  const createMutation = useMutation({
    mutationFn: (body: IEvent) => eventService.newEvent(body),
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
    mutationFn: (body: IEvent) => eventService.updateEvent(body),
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
      const formEdit = {
        eventId: editId,
        ...value
      }
      updateMutation.mutate(formEdit);
    } else {
      createMutation.mutate(value);
    }
  }
  const { data } = useQuery(['event'], () => eventService.getEventById(editId as number), {
    enabled: isEditIdValidNumber,
  });
  useEffect(() => {
    if (editId && data) {
      form.setFieldsValue(data.data);
    }
  }, [data]);
  return (
    <Modal title={editId ? `Chỉnh sửa sự kiện` : 'Tạo sự kiện mới'} centered open={open} width={1000} footer={false}>
      <Form
        form={form}
        name='basic'
        initialValues={{ remember: true }}
        onFinish={handleCreate}
        autoComplete='off'
        layout='vertical'
      >
        <Form.Item label='Tên sự kiện' name='name' rules={[{ required: true, message: 'Vui lòng nhập sự kiện' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Tên địa điểm' name='locationId' rules={[{ required: true, message: 'Vui lòng nhập địa điểm' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Ngày' name='date' rules={[{ required: true, message: 'Vui lòng nhập ngày' }]}>
          <DatePicker />
        </Form.Item>

        <Form.Item label='Bắt đầu lúc' name='startTime' rules={[{ required: true, message: 'Vui lòng nhập tgian bắt đầu lúc' }]}>
          <DatePicker />
        </Form.Item>

        <Form.Item label='Kết thúc lúc' name='endTime' rules={[{ required: true, message: 'Vui lòng nhập tgian kết thúc lúc' }]}>
          <DatePicker />
        </Form.Item>

        <Form.Item label='Chỗ ngồi' name='seatCount' rules={[{ required: true, message: 'Vui lòng nhập chỗ ngồi' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Giá vé' name='price' rules={[{ required: true, message: 'Vui lòng nhập giá vé' }]}>
          <Input />
        </Form.Item>

        <Form.Item label='Mô tả' name='description'>
          <Input.TextArea />
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

export default FormEvent;
