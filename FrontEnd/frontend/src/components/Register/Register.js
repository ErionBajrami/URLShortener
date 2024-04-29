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
      // Call register function  with email, password, and full name
      await axios.post('http://4.226.18.9:5001/api/User/signup', { email, password, fullName });
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
    <div className='register'>
        <form className="register-form" id="registerForm" onSubmit={handleSubmit}>
        <h2 className='text-blue-950 font-bold'>Register Here</h2>
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
        <p className='pt-4'>Already have an account? <Link to="/login" className="loginLink font-bold text-lg">Login Here!</Link></p>
      </form>
    </div>
  );
}

export default Register;
