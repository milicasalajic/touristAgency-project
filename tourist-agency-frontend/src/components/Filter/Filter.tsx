import React, { useState } from "react";
import { 
  getTouristPackagesAscending, 
  getTouristPackagesDescending, 
  getTouristPackages, 
  getTouristPackagesByDateRange 
} from "../../services/TouristPackages";
import { TouristPackagesGroup } from "../../model/TouristPackages";
import "./Filter.css";

interface TouristPackagesFiltersProps {
  onFilterChange: (filteredPackages: TouristPackagesGroup[]) => void;
}

const TouristPackagesFilters: React.FC<TouristPackagesFiltersProps> = ({ onFilterChange }) => {
  const [sortOrder, setSortOrder] = useState<string>("");
  const [startDate, setStartDate] = useState<string>("");
  const [endDate, setEndDate] = useState<string>("");

  const handleFilterChange = async (order: string) => {
    setSortOrder(order);

    try {
      let filteredPackages: TouristPackagesGroup[] = [];

      if (order === "asc") {
        filteredPackages = await getTouristPackagesAscending();
      } else if (order === "desc") {
        filteredPackages = await getTouristPackagesDescending();
      } else {
        filteredPackages = await getTouristPackages();
      }

      onFilterChange(filteredPackages);
    } catch (error) {
      console.error("Error fetching sorted packages:", error);
    }
  };

  const handleDateFilter = async () => {
    if (!startDate || !endDate) {
      alert("Molimo unesite oba datuma!");
      return;
    }

    try {
      const filteredPackages = await getTouristPackagesByDateRange(startDate, endDate);
      onFilterChange(filteredPackages);
    } catch (error) {
      console.error("Error fetching packages by date range:", error);
    }
  };

  return (
    <div className="filter-container">

      <label>Sortiraj po ceni:</label>
      <select value={sortOrder} onChange={(e) => handleFilterChange(e.target.value)}>
        <option value="">Bez sortiranja</option>
        <option value="asc">Cena rastuće</option>
        <option value="desc">Cena opadajuće</option>
      </select>

      <div className="date-filter">
        <label>Početni datum:</label>
        <input type="date" value={startDate} onChange={(e) => setStartDate(e.target.value)} />
        
        <label>Krajnji datum:</label>
        <input type="date" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
        
        <button onClick={handleDateFilter}>Filtriraj</button>
      </div>
      
    </div>
  );
};

export default TouristPackagesFilters;
