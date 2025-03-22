import React, { useState, useEffect } from "react";
import Navbar from "../Navbar/Navbar";
import TouristPackages from "../TouristPackages/TouristPackages";
import AboutUs from "../AboutUs/AboutUs";
import Contacts from "../Contacts/Contacts";
import { Link } from "react-router-dom";
import "./Home.css";
import { TouristPackagesGroup } from "../../model/TouristPackages";

function Home() {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem("token");
      setIsLoggedIn(!!token); // Postavi na true ako token postoji
    };

    checkAuth(); // Pokreni proveru odmah

    // Osluškuj promene u localStorage (prijava/odjava)
    window.addEventListener("storage", checkAuth);

    return () => {
      window.removeEventListener("storage", checkAuth);
    };
  }, []);

  return (
    <div>
      <Navbar onCategorySelect={function (selectedPackages: TouristPackagesGroup[]): void {
        throw new Error("Function not implemented.");
      } } /> 
      <div className="main-image">
        <img src="ta-main.JPG" alt="main" />
      </div>
      <TouristPackages />
      <AboutUs />
      <Contacts />
    </div>
  );
}

export default Home;



