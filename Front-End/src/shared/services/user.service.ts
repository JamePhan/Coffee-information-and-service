import { AxiosResponse } from 'axios';
import { httpsNoToken } from '../config/http.config';
import { IInforUser } from '../types/user.type';


class UserService {
    getAllUser(): Promise<AxiosResponse<IInforUser[]>> {
        return httpsNoToken.get('/User/List')
    }
    getUserById(userId: number): Promise<AxiosResponse<IInforUser>> {
        return httpsNoToken.get(`/User/Detail/${userId}`)
    }
    updateUser(body: IInforUser) {
        return httpsNoToken.put(`/User/Update`, body)
    }
    searchUser(search: string): Promise<AxiosResponse<IInforUser>> {
        return httpsNoToken.get(`/User/Search/${search}`)
    }
}

export const userService = new UserService()
