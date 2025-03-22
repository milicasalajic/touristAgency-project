import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { register } from "../../services/Auth";
import "./Registration.css";

function Register() {
  const [formData, setFormData] = useState({
    userName: "",
    password: "",
    email: "",
    role: "Tourist",
    name: "",
    lastName: "",
    phoneNumber: "",
  });
  const [message, setMessage] = useState<{ text: string; type: "success" | "error" } | null>(null);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleRegister = async () => {
    if (Object.values(formData).some((value) => value.trim() === "")) {
      setMessage({ text: "Sva polja moraju biti popunjena.", type: "error" });
      return;
    }

    setMessage(null);
    const success = await register(formData);

    if (success) {
      setMessage({ text: "Uspešno ste se registrovali!", type: "success" });
      setTimeout(() => navigate("/login"), 2000); // Prebacuje korisnika nakon 2 sekunde
    } else {
      setMessage({ text: "Došlo je do greške prilikom registracije.", type: "error" });
    }
  };

  return (
    <div className="register-container">
      <h1>Registracija</h1>
      {message && <p className={`message ${message.type}`}>{message.text}</p>}
      <input type="text" name="userName" placeholder="Korisničko ime" onChange={handleChange} />
      <input type="password" name="password" placeholder="Lozinka" onChange={handleChange} />
      <input type="email" name="email" placeholder="Email" onChange={handleChange} />
      <input type="text" name="name" placeholder="Ime" onChange={handleChange} />
      <input type="text" name="lastName" placeholder="Prezime" onChange={handleChange} />
      <input type="text" name="phoneNumber" placeholder="Telefon" onChange={handleChange} />
      <button className="register-btn" onClick={handleRegister}>Registruj se</button>
    </div>
  );
}

export default Register;
