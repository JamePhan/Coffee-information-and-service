import { AxiosResponse } from 'axios';
import { httpsNoToken } from '../config/http.config';
import { IRequest } from '../types/request.type';

class RequestService {
    getAllRequest(): Promise<AxiosResponse<IRequest[]>> {
        return httpsNoToken.get('/Waiting/List')
    }
}

export const requestService = new RequestService()
