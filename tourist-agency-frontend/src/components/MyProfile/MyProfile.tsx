import { useState, useEffect } from "react";
import userService from "../../services/User";
import { getUserReservations } from "../../services/Reservation";
import { getTouristPackage } from "../../services/TouristPackages"; 
import { AllReservationGroup } from "../../model/AllReservations";
import "./MyProfile.css";

const UpdateUserForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    lastName: "",
    userName: "",
    email: "",
    phoneNumber: "",
    userPhoto: "",
    oldPassword: "",
    newPassword: "",
    confirmNewPassword: "",
  });
  const [errorMessage, setErrorMessage] = useState("");
  const [successMessage, setSuccessMessage] = useState("");
  const [reservations, setReservations] = useState<AllReservationGroup[]>([]); 
  const [loadingReservations, setLoadingReservations] = useState<boolean>(true);

  // Funkcija za ažuriranje podataka korisnika
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setErrorMessage("");
    setSuccessMessage("");

    // Filtriraj podatke – ne šaljemo prazne stringove
    const filteredData = Object.fromEntries(
      Object.entries(formData).map(([key, value]) => [key, value.trim() === "" ? null : value])
    );

    try {
      await userService.updateUser(filteredData);
      setSuccessMessage("Uspešno ste ažurirali podatke.");
    } catch (error) {
      console.error("Greška:", error);
      setErrorMessage("Došlo je do greške pri ažuriranju. Pokušajte ponovo.");
    }
  };

  useEffect(() => {
    const fetchReservations = async () => {
      setLoadingReservations(true);
      const userReservations = await getUserReservations();
      
      const updatedReservations = await Promise.all(
        userReservations.map(async (reservation) => {
          try {
            const packageData = await getTouristPackage(reservation.touristPackageId);
            return { ...reservation, packageName: packageData.name }; // Dodajemo naziv paketa u rezervaciju
          } catch (error) {
            console.error("Greška pri učitavanju paketa:", error);
            return { ...reservation, packageName: "Nepoznat paket" }; 
          }
        })
      );

      setReservations(updatedReservations);
      setLoadingReservations(false);
    };

    fetchReservations();
  }, []);

  return (
    <div className="myProfile">
      <h2>Ažuriranje profila</h2>
      {errorMessage && <p style={{ color: "red" }}>{errorMessage}</p>}
      {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}

      <form onSubmit={handleSubmit}>
        <input type="text" name="name" placeholder="Ime" value={formData.name} onChange={handleChange} />
        <input type="text" name="lastName" placeholder="Prezime" value={formData.lastName} onChange={handleChange} />
        <input type="text" name="userName" placeholder="Korisničko ime" value={formData.userName} onChange={handleChange} />
        <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} />
        <input type="text" name="phoneNumber" placeholder="Telefon" value={formData.phoneNumber} onChange={handleChange} />
        <input type="password" name="oldPassword" placeholder="Stara lozinka" value={formData.oldPassword} onChange={handleChange} />
        <input type="password" name="newPassword" placeholder="Nova lozinka" value={formData.newPassword} onChange={handleChange} />
        <input type="password" name="confirmNewPassword" placeholder="Ponovite novu lozinku" value={formData.confirmNewPassword} onChange={handleChange} />
        <button type="submit">Sačuvaj promene</button>
      </form>

      {/* Prikaz rezervacija ako je korisnik Tourista */}
      <div className="userReservations">
        <h3>Moje rezervacije</h3>
        {loadingReservations ? (
          <p>Učitavanje rezervacija...</p>
        ) : reservations.length === 0 ? (
          <p>Nemate nijednu rezervaciju.</p>
        ) : (
          <ul>
            {reservations.map((reservation) => (
              <li key={reservation.id}>
                <strong>{reservation.name} {reservation.lastName}</strong> - Paket: {reservation.packageName} - Cena: {reservation.finalPrice}
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
};

export default UpdateUserForm;
