import { AllReservationGroup } from "../model/AllReservations";
import { ReservationGroup } from "../model/Reservations";

const apiUrl = process.env.REACT_APP_RESERVATION_API; 

export const createReservation = async (reservationData: ReservationGroup): Promise<boolean> => {
  try {
    const response = await fetch(`${apiUrl}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reservationData),
    });

    if (!response.ok) {
      throw new Error("Greška prilikom kreiranja rezervacije.");
    }

    return true;
  } catch (error) {
    console.error("Greška prilikom slanja rezervacije:", error);
    return false; 
  }
};

export const getReservationsForPackage = async (packageId: string): Promise<AllReservationGroup[]> => {
    try {
      const response = await fetch(`${apiUrl}/reservations-by-packages/${packageId}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`, 
        },
      });
  
      if (!response.ok) {
        throw new Error("Greška prilikom preuzimanja rezervacija.");
      }
  
      const data = await response.json();
  
      return data; 
    } catch (error) {
      console.error("Greška prilikom dobijanja rezervacija:", error);
      return []; 
    }
};

  export const getUserReservations = async (): Promise<AllReservationGroup[]> => {
    try {
      const response = await fetch(`${apiUrl}/my-reservations`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`, 
        },
      });
  
      if (!response.ok) {
        throw new Error("Greška prilikom preuzimanja mojih rezervacija.");
      }
  
      const data = await response.json();
  
      return data; 
    } catch (error) {
      console.error("Greška prilikom dobijanja mojih rezervacija:", error);
      return []; 
    }
};
  