export interface IEvent {
  eventId: number;
  name: string;
  address?: string;
  date: string;
  imageUrl: string;
  description: string;
  startTime: string;
  endTime: string;
  seatCount: number;
  price: number;
  coffeeShopName: string;
}
export interface IEventAdd {
  eventId: number;
  name: string;
  address?: string;
  date: string;
  imageUrl: string;
  description: string;
  startTime: string;
  endTime: string;
  seatCount: number;
  price: number;
  coffeeShopName: string;
}