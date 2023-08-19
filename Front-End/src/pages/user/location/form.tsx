import { useAppSelector } from '@/hooks/useRedux';
import { useMutation } from '@tanstack/react-query';
import { Button, Form, Input, message, Modal, Row, Col } from 'antd';
import { useForm } from 'antd/lib/form/Form';
import { locationService } from 'src/shared/services/location.service';
import { ILocation, ILocationAdd } from 'src/shared/types/location.type';

interface Props {
  editId?: number;
  open: any;
  setOpen: any;
  refetch: any;
}
const FormLocation = ({ editId, open, setOpen, refetch }: Props) => {
  const [form] = useForm();
  const { user } = useAppSelector(state => state.appSlice);
  const createMutation = useMutation({
    mutationFn: (body: ILocationAdd) => locationService.newLocation(body),
    onSuccess(data, _variables, _context) {
      if (!data) return;
      message.success('Tạo thành công');
      setOpen(false);
      refetch();
    },
    onError(error, variables, context) {
      message.error('Tạo không thành công');
    },
  });
  function handleCreate(value: any) {
    const formCreate = {
      address: value.address,
      userId: user?.id!,
    };
    createMutation.mutate(formCreate);
  }
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
        <Form.Item label='Tên' name='address' rules={[{ required: true, message: 'Vui lòng nhập' }]}>
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
