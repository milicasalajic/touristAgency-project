import { useNavigate } from "react-router-dom";

const apiUrl = process.env.REACT_APP_USER_API;
const token = localStorage.getItem("token");

const login = async (username:string, password:string): Promise<boolean> => {
    return fetch(`${apiUrl}/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({ username, password }),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Failed to fetch the member data");
            }
            return response.text();
        })
        .then((data) => {
            localStorage.setItem("token", data);
            return true;
        })
        .catch((error) => {
            console.error("Error fetching the member data:", error);
            return false;
        });
  };

   const register = async (userData: {
    userName: string;
    password: string;
    email: string;
    role: string;
    name: string;
    lastName: string;
    phoneNumber: string;
    
}): Promise<boolean> => {
    return fetch(`${apiUrl}/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(userData),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("An error occurred during registration");
            }
            return response.text();
        })
        .then(() => {
            return true;
        })
        .catch((error) => {
            console.error("Error during registration:", error);
            return false;
        });
};
export {login, register};