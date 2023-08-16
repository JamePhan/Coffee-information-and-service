import { AxiosResponse } from 'axios';
import { httpsNoToken } from '../config/http.config';
import { IFollowing, IFollowingAdd } from '../types/following.type';


class FollowingService {
    getCustomerList(customerId: number): Promise<AxiosResponse<IFollowing[]>> {
        return httpsNoToken.get(`/Follow/CustomerList/${customerId}`)
    }
    getUserList(userId: number): Promise<AxiosResponse<IFollowing[]>> {
        return httpsNoToken.get(`/Follow/UserList/${userId}`)
    }
    newFollowing(body: IFollowingAdd) {
        return httpsNoToken.post(`/Follow/Follow`, body)
    }
}

export const followingService = new FollowingService()
