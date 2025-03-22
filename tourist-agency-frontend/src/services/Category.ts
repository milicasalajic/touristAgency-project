import { CategoryGroup } from "../model/Category"; 
import { TouristPackagesGroup } from "../model/TouristPackages";

const apiUrl = process.env.REACT_APP_CATEGORY_API;

export const getCategories = async (): Promise<CategoryGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}`, {
      method: "GET", 
      headers: {
        "Content-Type": "application/json", 
      },
    });

    if (!response.ok) {
      throw new Error("Failed to fetch the categories data.");
    }

    const data: CategoryGroup[] = await response.json(); 
    return data;
  } catch (error) {
    console.error("Error fetching categories:", error);
    throw error; 
  }
};
export const addCategory = async (category: { name: string }): Promise<CategoryGroup> => {
  const response = await fetch(`${apiUrl}/add`, {
      method: "POST",
      headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
      body: JSON.stringify(category),
  });

  if (!response.ok) {
      throw new Error("Failed to add category.");
  }

  return await response.json(); 
};
export const getTouristPackagesByCategory = async (categoryId: string): Promise<TouristPackagesGroup[]> => {
  try {
    const response = await fetch(`${apiUrl}/${categoryId}/packages`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error("Failed to fetch tourist packages.");
    }

    const data: TouristPackagesGroup[] = await response.json(); 
    return data;
  } catch (error) {
    console.error("Error fetching tourist packages:", error);
    throw error;
  }
};