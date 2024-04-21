import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { getToken } from './AuthService'; // Import your authentication context

function PrivateOutlet() {
  // Check if user is authenticated by verifying token existence
  const isAuthenticated = getToken() != null;
  console.log("getToken", getToken());

return isAuthenticated ? <Outlet /> : <Navigate to="/login"/>
};

export default PrivateOutlet;
