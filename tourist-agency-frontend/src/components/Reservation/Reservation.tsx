import React, { useEffect, useState } from "react";
import { createReservation } from "../../services/Reservation";
import { ReservationGroup } from "../../model/Reservations";
import "./Reservation.css";
import { PaymentMethod } from "../../enums/PaymentMethod";

interface ModalProps {
  packageId: string;
  onClose: () => void;
}

const ReservationModal: React.FC<ModalProps> = ({ packageId, onClose }) => {
  useEffect(() => {
    console.log("Prosleđeni packageId:", packageId);  // Ispisuje se vrednost packageId svaki put kada se komponenta renderuje
  }, [packageId]);

  const [message, setMessage] = useState<string | null>(null); // Stanje za poruku
  const [isSuccess, setIsSuccess] = useState<boolean | null>(null); // Da li je rezervacija uspela


  const [formData, setFormData] = useState<ReservationGroup>({
    name: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    jmbg: "",
    bedCount: 1,
    touristPackageId: packageId,
    paymentMethod: PaymentMethod.PaymentCard,
    otherEmails: [],
    discountCode: "",
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;

    if (name === "otherEmails") {
      const emails = value.split(",").map(email => email.trim());
      setFormData({ ...formData, [name]: emails });
    } else if (name === "bedCount") {
      let bedCountValue = parseInt(value);
      // Ograničavamo broj kreveta na minimum 1 i maksimum 3
      bedCountValue = Math.max(1, Math.min(bedCountValue, 3)); // Osiguravamo da broj bude između 1 i 3
      setFormData({ ...formData, [name]: bedCountValue });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Poslati podaci:", formData);  // Logovanje podataka
    const success = await createReservation(formData);
    if (success) {
      setMessage("Rezervacija uspešna!");
      setIsSuccess(true);
      onClose();
    } else {
      setMessage("Došlo je do greške prilikom rezervacije.");
      setIsSuccess(false);
    }
  };

  const handleCloseMessageModal = () => {
    setMessage(null);
    setIsSuccess(null);
  };
  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <h2>Rezerviši Putovanje</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="text"
            name="name"
            placeholder="Ime"
            value={formData.name}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="lastName"
            placeholder="Prezime"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
          <input
            type="email"
            name="email"
            placeholder="Email"
            value={formData.email}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="phoneNumber"
            placeholder="Telefon"
            value={formData.phoneNumber}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="jmbg"
            placeholder="JMBG"
            value={formData.jmbg}
            onChange={handleChange}
            required
          />
          <input
            type="number"
            name="bedCount"
            placeholder="Broj kreveta"
            value={formData.bedCount}
            onChange={handleChange}
            required
            min={1}  // Ograničavamo minimalnu vrednost na 1
            max={3}  // Ograničavamo maksimalnu vrednost na 3
          />
          <select
            name="paymentMethod"
            value={formData.paymentMethod}
            onChange={handleChange}
          >
            <option value="PaymentCard">Kartica</option>
            <option value="Cash">Keš</option>
          </select>
          <input
            type="text"
            name="discountCode"
            placeholder="Kod za popust (ako postoji)"
            value={formData.discountCode}
            onChange={handleChange}
          />
          <input
            type="text"
            name="otherEmails"
            placeholder="Ostali emailovi (odvojeni zarezom)"
            value={formData.otherEmails.join(", ")}
            onChange={handleChange}
          />
          <button type="submit">Potvrdi rezervaciju</button>
          <button type="button" onClick={onClose}>
            Otkaži
          </button>
        </form>
      </div>
       {/* Modal za poruku */}
       {message && (
        <div className={`message-modal ${isSuccess ? "success" : "error"}`}>
          <div className="message-content">
            <p>{message}</p>
            <button onClick={handleCloseMessageModal}>Zatvori</button>
          </div>
        </div>
      )}
    </div>
  );
};

export default ReservationModal;
