export interface IAuthenticated {
  token: string
}
export interface IInforUser {
  userId: number;
  address: string;
  email: string;
  phone: string;
  coffeeShopName: string;
  avatar: string,
}
export interface IUserRegister {
  fullname: string;
  username: string;
  email: string;
  phone: string;
  password: string;
  avatar: string,
}
export interface IInforUserStored {
  id: string;
  profileId: string;
  role: string;
  name: string;
  phone: string;
  email: string;
  address: string;
  aud: string;
  iss: string;
  nbf: number;
  exp: number;
  iat: number;
}