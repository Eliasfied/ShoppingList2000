import https from "./https";

export const registerUser = async (
  name: string,
  email: string,
  password: string
) => {
  return await https.post("/api/authentication/register", {
    name: name,
    email: email,
    password: password,
  });
};

export const loginUser = async (email: string, password: string) => {
  return await https.post("/api/authentication/login", {
    email: email,
    password: password,
  });
};

export const logoutUser = async () => {
  return await https.get("/api/authentication/logout");
};
