import { TouristPackagesGroup } from "../model/TouristPackages"; 
import { TouristPackageGroup } from "../model/TouristPackage";
import { UpdateTouristPackage } from "../model/UpdateTouristPackage";
import { InsertTouristPackage } from "../model/InsertTouristPackage"; 
import { TripsGroup } from "../model/Trips";

const apiUrl = process.env.REACT_APP_TOURIST_PACKAGES_API;

export const getTouristPackages = async (): Promise<TouristPackagesGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}/packages`, {
      method: "GET", 
      headers: {
        "Content-Type": "application/json", 
      },
    });
    console.log(response);
    if (!response.ok) {
      throw new Error("Failed to fetch the packages data.");
    }

    const data: TouristPackagesGroup[] = await response.json(); 
    return data;
  } catch (error) {
    console.error("Error fetching tourist packages:", error);
    throw error; 
  }
};

export const getTouristPackagesAscending = async (): Promise<TouristPackagesGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}/filter-by-price-ascending`, {
      method: "GET", 
      headers: {
        "Content-Type": "application/json", 
      },
    });
    console.log(response);
    if (!response.ok) {
      throw new Error("Failed to fetch the packages data.");
    }

    const data: TouristPackagesGroup[] = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching tourist packages:", error);
    throw error; 
  }
};

export const getTouristPackagesDescending = async (): Promise<TouristPackagesGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}/filter-by-price-descending`, {
      method: "GET", 
      headers: {
        "Content-Type": "application/json", 
      },
    });
    console.log(response);
    if (!response.ok) {
      throw new Error("Failed to fetch the packages data.");
    }

    const data: TouristPackagesGroup[] = await response.json(); 
    return data;
  } catch (error) {
    console.error("Error fetching tourist packages:", error);
    throw error;
  }
};

export const getTouristPackage = async (id: string): Promise<TouristPackageGroup> => {
  try {
    const response = await fetch(`${apiUrl}/${id}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json", 
      },
    });
    console.log(response);
    if (!response.ok) {
      throw new Error("Failed to fetch the package data.");
    }

    const data: TouristPackageGroup = await response.json(); 
    return data;
  } catch (error) {
    console.error("Error fetching tourist package:", error);
    throw error;
  }
};

export const getTouristPackagesByDateRange = async (startDate: string, endDate: string): Promise<TouristPackagesGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}/filter-by-date-range?startDate=${startDate}&endDate=${endDate}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Failed to fetch the packages data.");
    }

    const data: TouristPackagesGroup[] = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching tourist packages by date range:", error);
    throw error;
  }
};

export const deleteTouristPackage = async (packageId: string) => {
  try {
    const response = await fetch(`${apiUrl}/delete/${packageId}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`, 
      },
    });

    if (!response.ok) {
      throw new Error("Failed to delete the package.");
    }

    return await response.json(); 
  } catch (error) {
    console.error("Error deleting tourist package:", error);
    throw error;
  }
};

export const updateTouristPackage = async (packageId: string, updatedPackage: UpdateTouristPackage) => {
  try {
    const response = await fetch(`${apiUrl}/update-package/${packageId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`, 
      },
      body: JSON.stringify(updatedPackage),
    });

    if (!response.ok) {
      throw new Error("Failed to update the package.");
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error updating tourist package:", error);
    throw error; 
  }
};

export const addTripsToPackage = async (touristPackageId: string, trips: TripsGroup[]) => {
  console.log("Tourist Package ID:", touristPackageId);  // Dodaj logovanje
  try {
    const response = await fetch(`${apiUrl}/addTrips/${touristPackageId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`, 
      },
      body: JSON.stringify(trips),
    });

    if (!response.ok) {
      throw new Error("Failed to add trips to the package.");
    }

    const data = await response.text();
    console.log("Trips added successfully:", data);
    return data; 
  } catch (error) {
    console.error("Error adding trips to package:", error);
    throw error;
  }
};

export const addTouristPackage = async (newTouristPackage: InsertTouristPackage) => {
  try {
    const response = await fetch(`${apiUrl}/add`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`, 
      },
      body: JSON.stringify(newTouristPackage), 
    });

    if (!response.ok) {
      throw new Error("Failed to add the tourist package.");
    }

    const data = await response.json();
    return data; 
  } catch (error) {
    console.error("Error adding tourist package:", error);
    throw error; 
  }
};
