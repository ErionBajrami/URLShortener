import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import './Login.scss'; 
import { Link } from 'react-router-dom';
import axios from 'axios';


const API = process.env.API_URL;

function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loggedIn, setLoggedIn] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`https://localhost:7295/api/User/login`, { email, password });
      const token = response.data;
      console.log('Login successful! Token:', token);
      setError('');
      localStorage.setItem('token', token);
      setLoggedIn(true)
    } catch (error) {
      setError('Email or password is incorrect');
    }
  };

  if (loggedIn) {
    return <Navigate to="/" />;
  }

  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Email:</label>
        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
      </div>
      <div className="form-group">
        <label>Password:</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      </div>
      {error && <div className="error-message">{error}</div>}
      <button type="submit" className="submit-btn">Login</button>
      <p>Don't have an account? <Link to="/register" className="registerLink">Register here</Link></p>
    </form>
  );
}

export default LoginForm;


