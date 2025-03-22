export interface UpdateTouristPackage {
    name?: string;
    description?: string;
    duration?: number;
    dateOfDeparture?: string; 
    returnDate?: string;       
    images?: string[];
    schedule?: string;
    priceIncludes?: string;
    priceDoesNotIncludes?: string;
    transportation?: number;  
}