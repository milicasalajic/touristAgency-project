import { PaymentMethod } from "../enums/PaymentMethod";

   
    export interface AllReservationGroup {
        id: string;
        touristPackageId: string; // Guid se mapira kao string
        bedCount: number;
        name: string;
        lastName: string;
        email: string;
        phoneNumber: string;
        jmbg: string;
        otherEmails: string[];
        paymentMethod: PaymentMethod;
        discountCode?: string;
        finalPrice: number;
        packageName?: string; 
      }
  