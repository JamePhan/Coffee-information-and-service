export interface IFollowing {
    followingId: number;
    customer: Customer;
    user: User;
}

interface User {
    userId: number;
    address: string;
    email: string;
    phone: string;
    coffeeShopName: string;
    avatar: string;
}

interface Customer {
    customerId: number;
    name: string;
    phone: string;
    address: string;
    email: string;
    avatar: string;
}

export interface IFollowingAdd {
    followingId: string;
    customer: CustomerAdd;
    user: UserAdd;
}

interface UserAdd {
    userId: string;
}

interface CustomerAdd {
    customerId: string;
}