import { TransportationType } from '../enums/TransportationType';

export interface TouristPackagesGroup {
    id: number;
    name: string;
    dateOfDeparture: string;
    returnDate: string;
    basePrice: number;
    firstImage: string;
    transportation: number;
}