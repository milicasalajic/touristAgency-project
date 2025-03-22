export interface TouristPackageGroup {
    id: string;
    name: string;
    description: string;
    duration: number;
    dateOfDeparture: string; 
    returnDate: string;
    images: string[];
    schedule: string;
    priceIncludes: string;
    priceDoesNotIncludes: string;
    category: {
        id: string;
        name: string;
    };
    destination: {
        id: string;
        name: string;
        hotel: string;
        hotelImages: string[];
        description: string;
    };
    trips: {
        id: string;
        name: string;
        description: string;
        price: number;
    }[];
    transportation: number; 
    basePrice: number;
    roomPrices: number[];
}

