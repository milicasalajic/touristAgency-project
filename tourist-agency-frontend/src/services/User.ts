const API_URL = process.env.REACT_APP_USER_API;

const updateUser = async (userData: any) => {
  const token = localStorage.getItem("token");

  const response = await fetch(`${API_URL}/update-user`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(userData),
  });

  if (!response.ok) {
    throw new Error("Failed to update user data");
  }

  return response.json();
};

export default {
  updateUser,
};
