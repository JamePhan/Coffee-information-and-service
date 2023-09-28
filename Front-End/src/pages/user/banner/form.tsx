import InputUpload from '@/components/common/UploadInput';
import { useAppSelector } from '@/hooks/useRedux';
import { useMutation, useQuery } from '@tanstack/react-query';
import { Button, Form, Input, message, Modal, Row, Col } from 'antd';
import { useForm } from 'antd/lib/form/Form';
import { useEffect, useState } from 'react';
import { bannerService } from 'src/shared/services/banner.service';
import { IBanner } from 'src/shared/types/banner.type';

interface Props {
  editId?: number;
  open: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  refetch: () => Promise<void>;
  onFormSubmit: () => void;
}

const FormBanner: React.FC<Props> = ({ editId, open, setOpen, refetch, onFormSubmit }: Props) => {
  const [form] = useForm();
  const { user } = useAppSelector(state => state.appSlice);
  const isEditIdValidNumber = typeof editId === 'number';
  const [actionPromise, setActionPromise] = useState<Promise<void> | null>(null);

  const createMutation = useMutation({
    mutationFn: (body: IBanner) => bannerService.newBanner(body),
    onSuccess: () => {
      message.success('Tạo thành công');
      setOpen(false); // Đóng modal sau khi thành công
      refetch();
    },
    onError: () => {
      message.error('Tạo không thành công');
    },
  });

  const updateMutation = useMutation({
    mutationFn: (body: IBanner) => bannerService.updateBanner(body),
    onSuccess: () => {
      message.success('Cập nhật thành công');
      setOpen(false); // Đóng modal sau khi thành công
      refetch();
    },
    onError: () => {
      message.error('Cập nhật không thành công');
    },
  });


  useEffect(() => {
    if (actionPromise) {
      actionPromise.then(() => {
        setOpen(false); // Đóng modal sau khi hoàn thành hành động
      });
    }
  }, [actionPromise, setOpen]);

  function handleCreate(value: any) {
    if (editId) {
      const formEdit: IBanner = {
        bannerId: editId,
        userId: user?.profileId || 0, // Sử dụng userId thay vì accountId
        ...value,

      };

      updateMutation
        .mutateAsync(formEdit)
        .then(() => {
          setActionPromise(null);
          onFormSubmit(); // Gọi onFormSubmit khi form được submit thành công
        })
        .catch(() => {
          setActionPromise(null);
        });
    } else {
      const formCreate: IBanner = {
        userId: user?.profileId || 0,
        ...value,
      };


      createMutation
        .mutateAsync(formCreate)
        .then(() => {
          setActionPromise(null);
          onFormSubmit(); // Gọi onFormSubmit khi form được submit thành công
        })
        .catch(() => {
          setActionPromise(null);
        });
    }
  }

  const { data } = useQuery(['Banner'], () => bannerService.getBannerById(editId as number), {
    enabled: isEditIdValidNumber,
  });

  useEffect(() => {

    if (editId && data) {
      form.setFieldsValue(data.data);
    }
  }, [data, editId, form]);

  return (
    <Modal title={editId ? `Chỉnh sửa banner` : 'Tạo banner mới'} centered visible={open} width={1000} footer={null} onCancel={() => setOpen(false)}>
      <Form form={form} name='basic' initialValues={{ remember: true }} onFinish={handleCreate} autoComplete='off' layout='vertical'>
        <Form.Item label='Ảnh' name='imageUrl' rules={[{ required: true, message: 'Vui lòng nhập ảnh' }]}>
          <InputUpload initSrc={data?.data.imageUrl} />
        </Form.Item>

        <Row justify='center' gutter={16}>
          <Col>
            <Form.Item style={{ textAlign: 'center' }}>
              <Button type='default' onClick={() => setOpen(false)}>
                Huỷ bỏ
              </Button>
            </Form.Item>
          </Col>
          <Col>
            <Form.Item style={{ textAlign: 'center' }}>
              <Button htmlType='submit'>
                {editId ? 'Chỉnh sửa' : 'Tạo mới'}
              </Button>
            </Form.Item>
          </Col>
        </Row>
      </Form>
    </Modal>
  );
};

export default FormBanner;
