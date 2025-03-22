import { TransportationType } from '../enums/TransportationType';
export interface Destination {
    Name: string;
    Description: string;
    Hotel: string;
    HotelImages: string[];
}
  
export interface InsertTouristPackage {
    name: string;
    description: string;
    duration: number;
    dateOfDeparture: string;
    returnDate: string;
    capacity: number;
    images: string[];
    schedule: string;
    priceIncludes: string;
    priceDoesNotIncludes: string;
    categoryId: string;
    destination: Destination;  
    transportation: number;
    basePrice: number;
    roomPrices: number[];
}
  