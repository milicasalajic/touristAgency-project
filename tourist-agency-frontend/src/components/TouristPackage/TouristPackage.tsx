import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getTouristPackage } from "../../services/TouristPackages";
import { TouristPackageGroup } from "../../model/TouristPackage";
import "./TouristPackage.css";
import Navbar from "../Navbar/Navbar";
import ReservationModal from "../Reservation/Reservation";

function TouristPackageDetail() {
  const { id } = useParams<{ id: string }>(); 
  const [packageData, setPackageData] = useState<TouristPackageGroup | null>(null);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false); 

  useEffect(() => {
    if (id) {
      getTouristPackage(id)
        .then((data) => {
          setPackageData(data);
          setLoading(false);
        })
        .catch((error) => {
          console.error("Error fetching tourist package details:", error);
          setLoading(false);
        });
    } else {
      console.error("ID is undefined");
      setLoading(false);
    }
  }, [id]);

  const handleReserveClick = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false); 
  };

  if (loading) return <p>Loading...</p>;
  if (!packageData) return <p>Package details not found.</p>;

  return (
    <div>
    
      <div className="page-container">
        <div className="package-detail-container">
          <div className="package-images">
            {packageData.images.slice(0, 3).map((image, index) => (
              <img key={index} src={`/${image}`} alt={`Package ${index + 1}`} />
            ))}
          </div>

          {/* Sekcija sa cenom, prevozom, trajanjem i dugmetom */}
          <div className="package-summary">
            <p className="price">Cena od: {packageData.basePrice} €</p>
            <img
              src={
                packageData.transportation === 1
                  ? "/bus.png"
                  : packageData.transportation === 0
                  ? "/plane.png"
                  : ""
              }
              alt="Transportation"
              className="transportation-img"
            />
            <p className="duration">Trajanje: {packageData.duration} dana</p>
            <button className="reserve-btn" onClick={handleReserveClick}>Rezerviši</button> {/* Otvori modal */}
          </div>

          <div className="package-info">
            <div className="package-header">
              <h1>{packageData.name}</h1>
              <p>{packageData.description}</p>
            </div>
            <div className="package-details">
              <div className="date-container">
                <p><strong>Datum polaska:</strong> {new Date(packageData.dateOfDeparture).getDate()}.{new Date(packageData.dateOfDeparture).getMonth() + 1}.{new Date(packageData.dateOfDeparture).getFullYear()}.</p>
                <p><strong>Datum povratka:</strong> {new Date(packageData.returnDate).getDate()}.{new Date(packageData.returnDate).getMonth() + 1}.{new Date(packageData.returnDate).getFullYear()}.</p>
              </div>
              <div className="schedule">
                <h3>Program putovanja:</h3>
                {packageData.schedule.split(";").map((item, index) => (
                <p key={index}>{item.trim()}</p>
                 ))}
              </div>
              <div className="price-inclusions">
                <div>
                  <h3>Cena uključuje:</h3>
                  {packageData.priceIncludes.split(',').map((item, index) => (
                  <p key={index}>{item.trim()}</p>
                  ))}
                </div>
                <div>
                  <h3>Cena ne uključuje:</h3>
                  {packageData.priceDoesNotIncludes.split(',').map((item, index) => (
                  <p key={index}>{item.trim()}</p>
                  ))}
                </div>
              </div>
            <h3>Destinacija:</h3>
            <p>{packageData.destination.name}</p>
            <p>{packageData.destination.hotel}</p>
            <p>{packageData.destination.description}</p>
            <div className="destination-images">
              {packageData.destination.hotelImages.map((image, index) => (
              <img key={index} src={`/${image}`} alt={`Hotel ${index + 1}`} />
               ))}
            </div>

            <h3>Fakultativni izleti:</h3>
            {packageData.trips.length > 0 ? (
            <ul>
              {packageData.trips.map((trip) => (
              <li key={trip.id}>
              <h4>{trip.name}</h4>
              <p>{trip.description}</p>
              <p>Cena: {trip.price} €</p>
              </li>
              ))}
            </ul>
            ) : (
            <p>Trenutno nema dostupnih izleta.</p> // Ovo je opcioni tekst koji se prikazuje ako je lista prazna
            )}
               
            </div>
          </div>
        </div>
      </div>
      {isModalOpen && id && <ReservationModal packageId={id} onClose={handleCloseModal} />}
    </div>
  );
}
export default TouristPackageDetail;

