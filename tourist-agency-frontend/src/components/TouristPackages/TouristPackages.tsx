import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom"; 
import { addTouristPackage, addTripsToPackage, deleteTouristPackage, getTouristPackage, getTouristPackages, updateTouristPackage } from "../../services/TouristPackages";
import { TouristPackagesGroup } from "../../model/TouristPackages";
import { TransportationType } from "../../enums/TransportationType";
import "./TouristPackages.css";
import TouristPackagesFilters from "../Filter/Filter";
import Navbar from "../Navbar/Navbar";
import { AllReservationGroup } from "../../model/AllReservations";
import { getReservationsForPackage } from "../../services/Reservation";
import { UpdateTouristPackage } from "../../model/UpdateTouristPackage";
import { InsertTouristPackage } from "../../model/InsertTouristPackage";

const getUserRoles = (): string[] => {
    const token = localStorage.getItem("token");
    if (!token) return [];

    try {
        const payload = JSON.parse(atob(token.split(".")[1])); // Dekodiranje JWT payload-a
        return payload["role"] ? [payload["role"]] :
               payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ? 
               [payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]] : [];
    } catch (error) {
        console.error("Error decoding token:", error);
        return [];
    }
};

const TouristPackages: React.FC = () => {
const [packages, setPackages] = useState<TouristPackagesGroup[]>([]);
const [loading, setLoading] = useState<boolean>(true);
const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
const [userRoles, setUserRoles] = useState<string[]>([]);
const [reservations, setReservations] = useState<AllReservationGroup[]>([]);
const [showModal, setShowModal] = useState<boolean>(false);  
const [showModal2, setShowModal2] = useState<boolean>(false);  
const [showModal3, setShowModal3] = useState<boolean>(false);  
const [showModalInsert, setShowModalInsert] = useState<boolean>(false);  
const [packageToEdit, setPackageToEdit] = useState<TouristPackagesGroup | null>(null);
const [trip, setTrip] = useState({ name: "", description: "", price: "" });
const [selectedPackageId, setSelectedPackageId] = useState<number | null>(null);
const [InsertPackage, setInsertPackage] = useState<InsertTouristPackage>({
  name:'',
  description: '',
  duration:0 ,
  dateOfDeparture: '',
  returnDate: '',
  capacity: 0,
  images: [],
  schedule: '',
  priceIncludes: '',
  priceDoesNotIncludes: '',
  categoryId: '',
  destination: {        
    Name: '',
    Description: '',
    Hotel: '',
    HotelImages: []
  },
  transportation: 0,
  basePrice:0,
  roomPrices: [],
}); 
const [updatedPackage, setUpdatedPackage] = useState<UpdateTouristPackage>({
  name: '',
  description: '',
  duration: undefined,
  dateOfDeparture: '',
  returnDate: '',
  images: [],
  schedule: '',
  priceIncludes: '',
  priceDoesNotIncludes: '',
  transportation: 0,
});

const handleSelectPackage = (packageId: number) => {
  setSelectedPackageId(packageId);
};

  const handleCategorySelect = (selectedPackages: TouristPackagesGroup[]) => {
    setPackages(selectedPackages);
  };

  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem("token");
      setIsLoggedIn(!!token);
      setUserRoles(getUserRoles());
    };

    getTouristPackages()
      .then((data) => {
        setPackages(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching tourist packages:", error);
        setLoading(false);
      });

    checkAuth();
    window.addEventListener("storage", checkAuth);

    return () => {
      window.removeEventListener("storage", checkAuth);
    };
  }, []);

  const hasAdminOrOrganizerRole = userRoles.includes("Admin") || userRoles.includes("Organizer");

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { id, value } = e.target;
    setTrip((prevTrip) => ({
      ...prevTrip,
      [id]: value, 
    }));
  };

  const handleDeletePackage = async (packageId: number) => {
    try {
      await deleteTouristPackage(packageId.toString());
      setPackages((prevPackages) => prevPackages.filter(pkg => pkg.id !== packageId)); 
 
    } catch (error) {
      console.error("Greška pri brisanju paketa:", error);
    }
  };

  const handleEditPackage = (packageId: number) => {
    const pkg = packages.find(pkg => pkg.id === packageId);
    if (pkg) {
      setPackageToEdit(pkg);
      setUpdatedPackage({
        name: pkg.name,
        dateOfDeparture: pkg.dateOfDeparture,
        returnDate: pkg.returnDate,
        transportation: pkg.transportation,
      });
      setShowModal2(true);
    }
  };

  const handleUpdatePackage = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!packageToEdit) return;
  
    try {
      const data = await updateTouristPackage(packageToEdit.id.toString(), updatedPackage);
      console.log("Paket je uspešno izmenjen:", data);
      setShowModal2(false); 
      setPackages(prevPackages => prevPackages.map(pkg => pkg.id === packageToEdit.id ? { ...pkg, ...updatedPackage } : pkg));
    } catch (error) {
      setShowModal2(false); 
    }
  };
 
  const fetchPackages = async () => {
    try {
      const data = await getTouristPackages(); // Pretpostavljam da imaš funkciju koja vraća sve pakete
      console.log("Ažurirani paketi:", data); // Dodaj logovanje
      setPackages(data);
    } catch (error) {
      console.error("Greška pri učitavanju paketa:", error);
    }
  }; 

  const handleAddTrips = async (packageId: number | null) => {
    if (!packageId) {
      console.warn("ID paketa nije postavljen!");
      return;
    }
  
    setShowModal3(true);
  
    if (!trip.name || !trip.description || !trip.price) {
      console.warn("Sva polja moraju biti popunjena!");
      return;
    }
  
    try {
      const formattedTrip = {
        ...trip,
        price: parseFloat(trip.price),
      };
  
      if (isNaN(formattedTrip.price)) {
        console.warn("Cena mora biti validan broj!");
        return;
      }
  
      const addedTrip = await addTripsToPackage(packageId.toString(), [formattedTrip]);
      console.log("Izlet uspešno dodat:", addedTrip);
  
      setShowModal3(false);
      setTrip({ name: "", description: "", price: "" });
      fetchPackages(); // Osveži podatke
  
    } catch (error) {
      console.error("Greška pri dodavanju izleta:", error);
    }
  };
  
  
  const handleAllReservations = async (packageId: number) => {
    try {
      const allReservations = await getReservationsForPackage(packageId.toString());
      console.log("Dobijene rezervacije:", allReservations);  
      setReservations(allReservations);
      setShowModal(true);
      console.log("Stanje reservations pre modala:", reservations);
    } catch (error) {
      console.error("Greška pri dobijanju rezervacija:", error);
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
   e.preventDefault();
   const touristPackage = {
    name: InsertPackage.name,
    description: InsertPackage.description,
    duration: InsertPackage.duration,
    dateOfDeparture: InsertPackage.dateOfDeparture,
    returnDate: InsertPackage.returnDate,
    capacity: InsertPackage.capacity,
    images: InsertPackage.images,
    schedule: InsertPackage.schedule,
    priceIncludes: InsertPackage.priceIncludes,
    priceDoesNotIncludes: InsertPackage.priceDoesNotIncludes,
    categoryId: InsertPackage.categoryId, 
    destination: InsertPackage.destination,
    transportation: InsertPackage.transportation,
    basePrice: InsertPackage.basePrice,
    roomPrices: InsertPackage.roomPrices,
  };

  try {
    const data = await addTouristPackage(touristPackage);
    console.log("Tourist package added:", data);
    setShowModalInsert(false);
  } catch (error) {
    console.error("Error:", error);
    setShowModalInsert(false);
  }
  };

  return (
    <div>
      <TouristPackagesFilters onFilterChange={setPackages} /> {}
      <Navbar onCategorySelect={handleCategorySelect} />
      {hasAdminOrOrganizerRole && (
        <div className="btn-container">
        <button className="btn-add-package" onClick={() => setShowModalInsert(true)} >
          Napravi paket
        </button>
      </div>
      )}

      <div className="package-list">
  
        {loading ? (
          <div className="card placeholder-card" aria-hidden="true">
            <img src="..." className="card-img-top" alt="..." />
            <div className="card-body">
              <h5 className="card-title placeholder-glow">
                <span className="placeholder col-6"></span>
              </h5>
              <p className="card-text placeholder-glow">
                <span className="placeholder col-7"></span>
                <span className="placeholder col-4"></span>
                <span className="placeholder col-4"></span>
                <span className="placeholder col-6"></span>
                <span className="placeholder col-8"></span>
              </p>
              <a className="btn btn-primary disabled placeholder col-6" aria-disabled="true"></a>
            </div>
          </div>
        ) : (
          packages.map((pkg) => (
            <div key={pkg.id} className="card">
              <Link to={`/tourist-package/${pkg.id}`} style={{ textDecoration: "none" }}>
                <img src={pkg.firstImage} className="card-img-top" alt={pkg.name} />
                <div className="card-body">
                  <h5 className="card-title">{pkg.name}</h5>
                  <p className="card-text">
                    {new Date(pkg.dateOfDeparture).getDate()}.{new Date(pkg.dateOfDeparture).getMonth() + 1} - 
                    {new Date(pkg.returnDate).getDate()}.{new Date(pkg.returnDate).getMonth() + 1}
                  </p>
                  <img src={pkg.transportation === 1 ? '/bus.png' : pkg.transportation === 0 ? '/plane.png' : ''} className="transportation-img" alt={pkg.name} />
                  <hr className="card-divider" />  
                  <p className="card-price"> Već od {pkg.basePrice} € po osobi</p>
                </div>
              </Link>

              {/* Dugmići ostaju van <Link> da ne prebacuju na drugu stranicu */}
              {hasAdminOrOrganizerRole && (
                <div className="card-footer">
                  <button onClick={() => handleDeletePackage(pkg.id)} className="btn btn-danger">
                    Obriši
                  </button>
                  <button onClick={() => handleEditPackage(pkg.id)} className="btn btn-danger">
                    Izmeni
                  </button>
                  <button
  onClick={() => {
    setSelectedPackageId(pkg.id);  // Postavi selectedPackageId na id trenutnog paketa
    console.log("ID paketa:", pkg.id); // Dodaj log
    handleAddTrips(pkg.id);  // Pozovi funkciju sa trenutnim id-om paketa
  }}
  className="btn btn-danger"
>
  Dodaj izlet
</button>      <button onClick={() => handleAllReservations(pkg.id)} className="btn btn-danger">
                    Sve rezervacije
                  </button>
                </div>
              )}
            </div>
            ))
          )}
      </div>


      {/* Modal za rezervacije */}
      {showModal && (
        <div className="modal-r" style={{ display: "block" }}>
          <div className="modal-dialog-r">
            <div className="modal-content-r">
              <div className="modal-header-r">
                <h5 className="modal-title-r">Sve rezervacije</h5>
                <button
                  type="button"
                  className="close-r"
                  onClick={() => setShowModal(false)}
                  >
                  <span>&times;</span>
                </button>
              </div>
              <div className="modal-body-r">
                {reservations.length > 0 ? (
                <ul>
                  {reservations.map((reservation) => (
                  <li key={reservation.id}>
                    <strong>{reservation.name} {reservation.lastName}</strong> - <br />
                    <strong>Email: {reservation.email}</strong> <br />
                    Telefon: {reservation.phoneNumber} <br />
                    Vrsta Sobe: {reservation.bedCount} <br />
                    Ostali putnici: {reservation.otherEmails} <br />
                    <strong>Ukupna cena: {reservation.finalPrice}€</strong>
                  </li>
                  ))}
                </ul>
                ) : (
                <p>Nemate rezervacija za ovaj paket.</p>
                )}
              </div>
            </div>
          </div>
        </div>  
      )}

      {/* Modal za izmenu paketa */}
      {packageToEdit &&  showModal2 &&(
        <div className="modal-r" style={{ display: "block" }}>
          <div className="modal-dialog-r">
            <div className="modal-content-r">
              <div className="modal-header-r">
                <h5 className="modal-title-r">Izmeni paket: {packageToEdit.name}</h5>
                <button type="button" className="close-r" onClick={() => setShowModal2(false)}>
                  <span>&times;</span>
                </button>
              </div>
              <form onSubmit={handleUpdatePackage}>
                <div className="modal-body-r">
                  <div className="form-group">
                    <label htmlFor="name">Naziv paketa</label>
                    <input
                      type="text"
                      id="name"
                      className="form-control"
                      value={updatedPackage.name}
                      onChange={(e) => setUpdatedPackage({ ...updatedPackage, name: e.target.value })}
                    />
                  </div>
                    <div className="form-group">
                      <label htmlFor="description">Opis paketa</label>
                      <textarea
                        id="description"
                        className="form-control"
                        value={updatedPackage.description}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, description: e.target.value })}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="dateOfDeparture">Datum polaska</label>
                      <input
                        type="date"
                        id="dateOfDeparture"
                        className="form-control"
                        value={updatedPackage.dateOfDeparture}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, dateOfDeparture: e.target.value })}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="returnDate">Datum povratka</label>
                      <input
                        type="date"
                        id="returnDate"
                        className="form-control"
                        value={updatedPackage.returnDate}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, returnDate: e.target.value })}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="transportation">Vrsta transporta</label>
                      <select
                        id="transportation"
                        className="form-control"
                        value={updatedPackage.transportation}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, transportation: parseInt(e.target.value) })}
                        >
                        <option value={0}>Avion</option>
                        <option value={1}>Autobus</option>
                      </select>
                    </div>
                    <div className="form-group">
                      <label htmlFor="schedule">Raspored</label>
                      <textarea
                        id="schedule"
                        className="form-control"
                        value={updatedPackage.schedule}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, schedule: e.target.value })}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="priceIncludes">Uključene usluge</label>
                      <textarea
                        id="priceIncludes"
                        className="form-control"
                        value={updatedPackage.priceIncludes}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, priceIncludes: e.target.value })}
                      />
                    </div>
                    <div className="form-group">
                      <label htmlFor="priceDoesNotIncludes">Neuključene usluge</label>
                      <textarea
                        id="priceDoesNotIncludes"
                        className="form-control"
                        value={updatedPackage.priceDoesNotIncludes}
                        onChange={(e) => setUpdatedPackage({ ...updatedPackage, priceDoesNotIncludes: e.target.value })}
                      />
                    </div>
                  </div>
                  <div className="modal-footer-r">
                    <button type="submit" className="btn btn-primary">
                      Sačuvaj
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        )}

        {/* Modal za dodavanje izleta */}
        {/* Modal za dodavanje izleta */}
{showModal3 && (
  <div className="modal-r" style={{ display: "block" }}>
    <div className="modal-dialog-r">
      <div className="modal-content-r">
        <div className="modal-header-r">
          <h5 className="modal-title-r">Dodaj izlet</h5>
          <button type="button" className="close-r" onClick={() => setShowModal3(false)}>
            <span>&times;</span>
          </button>
        </div>
        <div className="modal-body-r">
          <form onSubmit={(e) => e.preventDefault()}>  {/* Sprečava reload stranice */}
            <div className="form-group">
              <label htmlFor="name">Naziv izleta</label>
              <input
                type="text"
                id="name"
                className="form-control"
                placeholder="Unesite naziv izleta"
                value={trip.name}  // Povezano sa state-om
                onChange={handleInputChange}  // Poziva funkciju za ažuriranje state-a
              />
            </div>
            <div className="form-group">
              <label htmlFor="description">Opis izleta</label>
              <textarea
                id="description"
                className="form-control"
                placeholder="Unesite opis izleta"
                value={trip.description}
                onChange={handleInputChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="price">Cena izleta</label>
              <input
                type="text"
                id="price"
                className="form-control"
                placeholder="Unesite cenu izleta"
                value={trip.price}
                onChange={handleInputChange}
              />
            </div>
            <div className="modal-footer-r">
              <button
                type="button"
                className="btn btn-add"
                onClick={(e) => handleAddTrips(selectedPackageId)}
              >
                Dodaj izlet
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
)}


      {showModalInsert && (
        <div className="modal-i">
          <div className="modal-content-i">
            <h2>Napravi novi turistički paket</h2>
            <form onSubmit={handleSubmit}>
            <label>Ime paketa:</label>
            <input
              type="text"
            value={InsertPackage.name}
            onChange={(e) => setInsertPackage({ ...InsertPackage, name: e.target.value })}
            />
            <label>Opis:</label>
            <textarea
              value={InsertPackage.description}
              onChange={(e) => setInsertPackage({ ...InsertPackage, description: e.target.value })}
            />
            <label>Trajanje (u danima):</label>
            <input
              type="number"
              value={InsertPackage.duration}
              onChange={(e) => setInsertPackage({ ...InsertPackage, duration: Number(e.target.value) })}
            />
            <label>Datum polaska:</label>
            <input
              type="date"
              value={InsertPackage.dateOfDeparture}
              onChange={(e) => setInsertPackage({ ...InsertPackage, dateOfDeparture: e.target.value })}
            />
            <label>Datum povratka:</label>
            <input
              type="date"
              value={InsertPackage.returnDate}
              onChange={(e) => setInsertPackage({ ...InsertPackage, returnDate: e.target.value })}
            />
            <label>Kapacitet:</label>
            <input
              type="number"
              value={InsertPackage.capacity}
              onChange={(e) => setInsertPackage({ ...InsertPackage, capacity: Number(e.target.value) })}
            />
            <label>Slike (URL, odvoji zarezom):</label>
            <input
              type="text"
              value={InsertPackage.images.join(', ')}
              onChange={(e) => setInsertPackage({ ...InsertPackage, images: e.target.value.split(', ') })}
            />
            <label>Raspored:</label>
            <textarea
              value={InsertPackage.schedule}
              onChange={(e) => setInsertPackage({ ...InsertPackage, schedule: e.target.value })}
            />
            <label>Uključeno u cenu:</label>
            <textarea
              value={InsertPackage.priceIncludes}
              onChange={(e) => setInsertPackage({ ...InsertPackage, priceIncludes: e.target.value })}
            />
          <label>Nije uključeno u cenu:</label>
          <textarea
            value={InsertPackage.priceDoesNotIncludes}
            onChange={(e) => setInsertPackage({ ...InsertPackage, priceDoesNotIncludes: e.target.value })}
          />
          <label>Kategorija ID:</label>
          <input
            type="text"
            value={InsertPackage.categoryId}
            onChange={(e) => setInsertPackage({ ...InsertPackage, categoryId: e.target.value })}
          />
          <label>Destinacija:</label>
          <div>
            <label>Ime destinacije:</label>
            <input
              type="text"
              name="Name"  // Ovaj name atribut treba da odgovara polju unutar objekta destination
              value={InsertPackage.destination.Name}  // Pristupamo Name polju objekta destination
              onChange={(e) => 
              setInsertPackage({
                ...InsertPackage,
                destination: {
                  ...InsertPackage.destination,
                  Name: e.target.value,  // Ažuriramo samo Name polje
                }
              })
             }
            />
          </div>
          <div>
            <label>Opis destinacije:</label>
            <input
              type="text"
              name="Description"  // Ovaj name atribut treba da odgovara polju unutar objekta destination
              value={InsertPackage.destination.Description}  // Pristupamo Description polju objekta destination
              onChange={(e) => 
                setInsertPackage({
                ...InsertPackage,
                destination: {
                  ...InsertPackage.destination,
                  Description: e.target.value,  // Ažuriramo samo Description polje
                }
               })
              }
            />
          </div>
          <div>
            <label>Hotel:</label>
            <input
              type="text"
              name="Hotel"  // Ovaj name atribut treba da odgovara polju unutar objekta destination
              value={InsertPackage.destination.Hotel}  // Pristupamo Hotel polju objekta destination
              onChange={(e) => 
              setInsertPackage({
                ...InsertPackage,
                destination: {
                  ...InsertPackage.destination,
                  Hotel: e.target.value,  // Ažuriramo samo Hotel polje
                }
              })
              }
            />  
          </div>
          <div>
            <label>Slike hotela:</label>
            <input
              type="text"
              name="HotelImages"  // Ovaj name atribut treba da odgovara polju unutar objekta destination
              value={InsertPackage.destination.HotelImages.join(', ')}  // Prikazivanje slika kao stringa
              onChange={(e) => 
                setInsertPackage({
                  ...InsertPackage,
                  destination: {
                    ...InsertPackage.destination,
                    HotelImages: e.target.value.split(',').map(img => img.trim())  // Ažuriramo slike kao niz
                  }
                })
              }
            />
          </div>
          <label>Prevoz:</label>
            <input
              type="number"
              value={InsertPackage.transportation}
              onChange={(e) => setInsertPackage({
                ...InsertPackage,
                transportation: Number(e.target.value)  // Konvertuje vrednost u broj
               })}
            />
          <label>Osnovna cena:</label>
              <input
                type="number"
                value={InsertPackage.basePrice}
                onChange={(e) => setInsertPackage({ ...InsertPackage, basePrice: Number(e.target.value) })}
              />
         <label>Cene soba (format: jednokrevetna, dvokrevetna, trokrevetna...):</label>
            <input
              type="text"
              value={InsertPackage.roomPrices.join(', ')} 
              onChange={(e) => {
                const roomPrices = e.target.value
                .split(',')
                .map(price => Number(price.trim())) // Trimuj i pretvori u broj
                .filter(price => !isNaN(price)); // Ukloni nevalidne vrednosti

                setInsertPackage({ ...InsertPackage, roomPrices });
             }}
            />

            <button type="submit">Sačuvaj</button>
            <button type="button" onClick={() => setShowModalInsert(false)}>Odustani</button>
            </form>
          </div>
        </div>
        )}
      </div>
    );
  };

export default TouristPackages;
