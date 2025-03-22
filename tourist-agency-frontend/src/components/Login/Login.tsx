import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../../services/Auth";
import "./Login.css";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [badCredentials, setBadCredentials] = useState("");
  const navigate = useNavigate();

  const handleLogin = async () => {
    const success = await login(username, password);
    if (success) {
      navigate("/");
    } else {
      setBadCredentials("Korisničko ime ili lozinka nisu ispravni.");
    }
  };

  return (
    <div className="login-container">
      <div className="centered-content-wrap">
        <div className="centered-block">
          <h1>Prijava</h1>
          {badCredentials && <p className="error">{badCredentials}</p>}
          <ul>
            <li>
              <input
                type="text"
                placeholder="Korisničko ime"
                className="input-field"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </li>
            <li>
              <input
                type="password"
                placeholder="Lozinka"
                className="input-field"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </li>
            <li className="form-actions">
              <a className="register-link" onClick={() => navigate("/registration")}>
                Nemate profil? Napravite ga.
              </a>
              <button className="login-btn" onClick={handleLogin}>
                Prijava
              </button>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
}

export default Login;
