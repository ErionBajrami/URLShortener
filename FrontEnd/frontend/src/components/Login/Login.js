import React, { useState } from 'react';
import { Navigate } from 'react-router-dom';
import './Login.scss'; 
import { Link } from 'react-router-dom';
import axios from 'axios';
import { BASE_URL } from '../../config.js'; // Import the BASE_URL


function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loggedIn, setLoggedIn] = useState(false);
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(`${BASE_URL}/api/User/login`, { email, password });
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
    <div className='login'>
      <form className="login-form" id="loginForm" onSubmit={handleSubmit}>
        <h2>Sign in to your account</h2>
        <div className="form-group">
          <label>Email:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder='Email address' />
        </div>
        <div className="form-group">
          <label>Password:</label>
          <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder='Password' />
        </div>
        {error && <div className="error-message">{error}</div>}
        <button type="submit" className="submit-btn">Login</button>
        <p>Don't have an account? <Link to="/register" className="registerLink">Register here</Link></p>
      </form>
    </div>
  );
}

export default LoginForm;


