import React from 'react';
import './Navbar.scss';

const Navbar = () => {
  return (
    <div className="navbar">
        <div>
            <ul className='navbar-list'>
                <li>
                    <a href="/">Home</a>
                </li>
                <li>
                    <a href="/urls">Urls</a>
                </li>
                <li>
                    <a href="/search">Search</a>
                </li>
            </ul>
        </div>
        <div>
            <button className='login'>Log in</button>
        </div> 
    </div>
  )
}

export default Navbar