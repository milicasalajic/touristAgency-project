import "./Navbar.css";
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getCategories, addCategory, getTouristPackagesByCategory } from "../../services/Category";
import { CategoryGroup } from "../../model/Category";
import { Link } from "react-router-dom";
import { TouristPackagesGroup } from "../../model/TouristPackages";

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
// Definišite interfejs za props
interface NavbarProps {
    onCategorySelect: (selectedPackages: TouristPackagesGroup[]) => void;
  }

const Navbar: React.FC<NavbarProps> = ({ onCategorySelect }) => {
    const [categories, setCategories] = useState<CategoryGroup[]>([]);
    const [error, setError] = useState<string>("");
    const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
    const [userRoles, setUserRoles] = useState<string[]>([]);
    const navigate = useNavigate();
    const [isDiscountModalOpen, setIsDiscountModalOpen] = useState(false);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [newCategoryName, setNewCategoryName] = useState("");
  
    useEffect(() => {
      const fetchCategories = async () => {
        try {
          const data = await getCategories();
          setCategories(data);
        } catch (error) {
          setError("Greška prilikom učitavanja kategorija.");
        }
      };
  
      fetchCategories();
  
      const checkAuth = () => {
        const token = localStorage.getItem("token");
        setIsLoggedIn(!!token);
        setUserRoles(getUserRoles());
      };
  
      checkAuth();
      window.addEventListener("storage", checkAuth);
  
      return () => {
        window.removeEventListener("storage", checkAuth);
      };
    }, []);
  
    const hasAdminOrOrganizerRole = userRoles.includes("Admin") || userRoles.includes("Organizer");
  
    const handleCategoryClick = async (categoryId: string) => {
      try {
        const packages = await getTouristPackagesByCategory(categoryId);
        onCategorySelect(packages); // Prosleđujemo pakete ka parent komponenti
      } catch (error) {
        console.error("Greška pri filtriranju paketa:", error);
      }
    };
  
    const handleAddCategory = async () => {
      if (!newCategoryName.trim()) return;
  
      try {
        const newCategory = await addCategory({ name: newCategoryName });
        setCategories([...categories, newCategory]);
        setIsModalOpen(false);
        setNewCategoryName("");
      } catch (error) {
        console.error("Greška prilikom dodavanja kategorije:", error);
        alert("Došlo je do greške pri dodavanju kategorije.");
      }
    };
    const handleDiscountClick = () => {
      setIsDiscountModalOpen(true);
    };
  
    return (
      <div className="container">
        <nav className="navbar">
          <Link to="/" className="logo">
            <img src="ta-logo.JPG" alt="Logo" />
          </Link>

          <ul className="nav-links">
            <li className="nav-item dropdown">
              <a href="#" className="nav-link">KATEGORIJE</a>
              <ul className="dropdown-menu">
                {error ? (
                  <li><a className="dropdown-item">{error}</a></li>
                ) : categories.map((category) => (
                  <li key={category.id}>
                    <a className="dropdown-item" onClick={() => handleCategoryClick(String(category.id))}>{category.name}</a>
                  </li>
                ))}
                {hasAdminOrOrganizerRole && (
                  <li>
                    <a className="dropdown-item add-category" onClick={() => setIsModalOpen(true)}>
                      ➕ Dodaj kategoriju
                    </a>
                  </li>
                )}
              </ul>
            </li>
            <li><a className="nav-link" onClick={handleDiscountClick}>POPUSTI</a></li>
            <li><a className="nav-link" href="#about-us">O NAMA</a></li>
            {!isLoggedIn ? (
              <li><a className="btn" onClick={() => navigate("/login")}>PRIJAVA</a></li>
            ) : (
              <>
                <li><a href="/profile">MOJ PROFIL</a></li>
                <li><a className="btn logout-btn"
                  onClick={() => {
                    localStorage.removeItem("token");
                    setIsLoggedIn(false);
                    setUserRoles([]); // Resetuje role korisnika
                    navigate("/");
                    window.location.reload(); // Osvežava stranicu
                  }}
                  >ODJAVA
                  </a>
                </li>
              </>
            )}
          </ul>
        </nav>
  
        
        {isModalOpen && (
          <div className="modal-overlay">
            <div className="modal">
              <h2>Dodaj novu kategoriju</h2>
              <input
                type="text"
                placeholder="Unesite naziv kategorije"
                value={newCategoryName}
                onChange={(e) => setNewCategoryName(e.target.value)}
              />
              <div className="modal-buttons">
                <button onClick={() => setIsModalOpen(false)}>Otkaži</button>
                <button onClick={handleAddCategory}>Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {isDiscountModalOpen && (
          <div className="modal-overlay">
            <div className="modal">
              <h2>Popusti</h2>
              <p>{isLoggedIn ? "Vaš kod za popust: DISCOUNT_2024" : "Samo prijavljeni korisnici imaju mogućnost popusta."}</p>
              <button onClick={() => setIsDiscountModalOpen(false)}>Zatvori</button>
            </div>
          </div>
        )}
      </div>
    );
  };
  
  export default Navbar;
  