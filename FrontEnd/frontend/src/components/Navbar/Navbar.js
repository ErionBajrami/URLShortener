import React, {useState, useEffect} from 'react';
import axios from 'axios';
import './Navbar.scss';
import { Link } from 'react-router-dom';

const Navbar = () => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  console.log('isAuthenticated', isAuthenticated);
  const [isAdmin, setIsAdmin] = useState(false);
  const token = localStorage.getItem('token');

  useEffect(() => {
    // Check if user is authenticated by verifying token existence
    setIsAuthenticated(token !== null);
  }, [token]);

  useEffect(() => {
    const fetchData = async () => {
        try {
            // Check if the user is an admin
            const isAdminResponse = await axios.get(`http://localhost:5001/api/User/isAdmin?token=${token}`);
            setIsAdmin(isAdminResponse.data);
        } catch (error) {
            console.log("Error fetching data: ", error);
        }
    };
    fetchData();
}, [token]);

  const handleLogout = () => {
    // Clear token from storage upon logout
    localStorage.removeItem("token");
    setIsAuthenticated(false);
  };

  return (
    <div id="navbar" className="navbar bg-gray-900 text-white flex flex-row justify-between items-center px-6 py-4 text-lg">
      <div className="logo" id="logo">
        <img src="/static/images/Logo.svg" loading="lazy" alt="logo" />
      </div>
      <div>
        <ul className="navbar-list">
          <li className='pl-2 pr-2 hover:text-sky-200' id="item">
            <Link to="/">Home</Link>
          </li>
          <li  className='pl-2 pr-2 hover:text-sky-200' id="item">
            <Link to="/urls">Urls</Link>
          </li>
          <li  className='pl-2 pr-2 hover:text-sky-200' id="item">
            <Link to="/search">Search</Link>
          </li>
          {isAdmin && (
              <li  className='pl-2 pr-2 hover:text-sky-200' id="item">
               <Link to="/analytics">Analytics</Link>
             </li>
          )}
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
