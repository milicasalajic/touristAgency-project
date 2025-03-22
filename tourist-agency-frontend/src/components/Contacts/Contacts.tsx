import React from "react";
import "./Contacts.css";

const Contact = () => {
  return (
    <footer className="contact-container">
      <div className="contact-content">
        <p>📍 Adresa: Bulevar Ekspedicija 123, Beograd</p>
        <p>📞 Telefon: <a href="tel:+3811234567">+381 12 345 67</a></p>
        <p>✉️ Email: <a href="mailto:ekspedicija@gmail.com">ekspedicija@gmail.com</a></p>
        <p>© {new Date().getFullYear()} Ekspedicija Travel | Sva prava zadržana</p>
      </div>
    </footer>
  );
};

export default Contact;