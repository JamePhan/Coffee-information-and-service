import { AxiosResponse } from 'axios';
import { httpsNoToken } from '../config/http.config';
import { ILocation } from '../types/location.type';


class LocationService {
    getAllLocation(): Promise<AxiosResponse<ILocation[]>> {
        return httpsNoToken.get('/Location/List')
    }
    newLocation(body: {address: string, userId: number}) {
        return httpsNoToken.post("/Location/Add", body)
    }
    deleteLocation(id: number) {
        return httpsNoToken.delete(`/Location/Delete?id=${id}`)
    }
}

export const locationService = new LocationService()
