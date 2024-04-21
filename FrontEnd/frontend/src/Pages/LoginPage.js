import React from 'react';
import Login from '../components/Login/Login';
import Navbar from '../components/Navbar/Navbar';

const LoginPage = () => {
  return (
    <div>
      <Navbar />
      <div style={{paddingTop: "120px"}}>
      <Login/>
      </div>
    </div>
  )
}

export default LoginPage