import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';


function PrivateOutlet() {
  // Check if user is authenticated by verifying token existence
  const isAuthenticated = localStorage.getItem('token');

return isAuthenticated ? <Outlet /> : <Navigate to="/login"/>
};

export default PrivateOutlet;
