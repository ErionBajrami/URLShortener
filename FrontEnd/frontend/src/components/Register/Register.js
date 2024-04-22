import React, { useState } from 'react';
import axios from 'axios';
import { Navigate, Link } from 'react-router-dom';
import './Register.scss'; 

function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [fullName, setFullName] = useState('');
  const [error, setError] = useState('');
  const [registered, setRegistered] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Call register function from AuthService with email, password, and full name
      const response = await axios.post('http://localhost:5284/api/User/signup', { email, password, fullName });
      setRegistered(true); // Set registered status to true
      setError('');
    } catch (error) {
      setError('Registration failed. Please try again.');
    }
  };

  // If registered, redirect to the login page
  if (registered) {
    return <Navigate to="/login" />;
  }

  return (
    <form className="register-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label>Email:</label>
        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
      </div>
      <div className="form-group">
        <label>Password:</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      </div>
      <div className="form-group">
        <label>Full Name:</label>
        <input type="text" value={fullName} onChange={(e) => setFullName(e.target.value)} />
      </div>
      {error && <div className="error-message">{error}</div>}
      <button type="submit" className="submit-btn">Register</button>
      <p>Already have an account? <Link to="/login" className="loginLink">Login here</Link></p>
    </form>
  );
}

export default Register;
