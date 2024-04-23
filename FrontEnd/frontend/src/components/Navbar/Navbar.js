import React, { useState, useEffect } from "react";
import "./Navbar.scss";
import { Link } from "react-router-dom";

const Navbar = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  console.log("isAuthenticated", isAuthenticated);

  useEffect(() => {
    // Check if user is authenticated by verifying token existence
    const token = localStorage.getItem("token");
    setIsAuthenticated(token !== null);
  }, []);

  const handleLogout = () => {
    // Clear token from storage upon logout
    localStorage.removeItem("token");
    setIsAuthenticated(false);
  };

  return (
    <div className="navbar">
      <div className="logo">
        <img src="/static/images/Logo.svg" loading="lazy" />
      </div>
      <div>
        <ul className="navbar-list">
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/urls">Urls</Link>
          </li>
          <li>
            <Link to="/search">Search</Link>
          </li>
          <li>
            <input type="search" placeholder="Search..."/>
          </li>
        </ul>
      </div>
      <div className="login">
        {/* Conditionally render login or logout button */}
        {isAuthenticated ? (
          <Link to="/login">
            <button onClick={handleLogout}>
              Log out
            </button>
          </Link>
        ) : (
          <Link to="/login">
            <button>Log in</button>
          </Link>
        )}
      </div>
    </div>
  );
};

export default Navbar;
