import { PaymentMethod } from "../enums/PaymentMethod";

   
export interface ReservationGroup {
  touristPackageId: string; 
  bedCount: number;
  name: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  jmbg: string;
  otherEmails: string[];
  paymentMethod: PaymentMethod;
  discountCode?: string; 
}
  