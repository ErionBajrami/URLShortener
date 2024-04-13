import React from 'react';
import  './Homepage.scss';

const Homepage = () => {
    return (
        <div className ="homepage-container">
            <div className="logo-container">
                <img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRXXjpNyEttzp467chiflgYdYC9wgi4SP2Pg9InjvpGFA&s' alt="sdi" className="logo-image" />
            </div>
            <div className='login-container'>
                <button className="login-button">Login</button>
                <button className="login-button">Sign Up</button>
            </div>
        </div>
    );
};

export default Homepage;