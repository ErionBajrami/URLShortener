import React from 'react';
import './Navbar.scss';
import { Link } from 'react-router-dom';
import { logout, getToken } from '../../AuthService'; // Import getToken from AuthService

const Navbar = () => {
  // Check if user is authenticated
  const isAuthenticated = getToken() !== null;

  return (
    <div className="navbar">
      <div>
        <ul className='navbar-list'>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/urls">Urls</Link>
          </li>
          <li>
            <Link to="/search">Search</Link>
          </li>
        </ul>
      </div>
      <div>
        {/* Conditionally render login or logout button */}
        {!isAuthenticated && (
          <Link to="/login">
            <button className='login'>Log in</button>
          </Link>
        )}
        {isAuthenticated && (
          <button className='login' onClick={logout()}>Log out</button>
        )}
      </div> 
    </div>
  );
}

export default Navbar;
