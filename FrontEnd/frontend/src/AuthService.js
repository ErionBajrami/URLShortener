import axios from 'axios';


const login = async (email, password) => {
  try {
    const response = await axios.post('https://localhost:7295/api/User/login', { email, password });
    const token = response.data;
    localStorage.setItem('token', token);
    return token;
  } catch (error) {
    throw new Error('Login failed');
  }
};

const register = async (email, password, fullName) => {
  try {
    const response = await axios.post('https://localhost:7295/api/User/signup', { email, password, fullName });
    console.log('response from register', response)
    const token = response.data;
    localStorage.setItem('token', token);
    return token;
  } catch (error) {
    throw new Error('Registration failed');
  }
};

const logout = () => {
  // Clear token from storage upon logout
  localStorage.removeItem('token');
};

const getToken = () => {
  // Retrieve token from storage
  return localStorage.getItem('token');
};

export { login, register, logout, getToken };
