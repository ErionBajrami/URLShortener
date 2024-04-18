import React from 'react';
import  './Login.scss';
import { useHistory } from 'react-router-dom';

const Loginpage = () => {

    const history = useHistory();
        
    const navigateToCreateUrl = () => {
        history.push('./HomePage');
    };
      
    return (
        <div className ="Loginpage-container">
            <div className="logo-container">
                <img src='https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRXXjpNyEttzp467chiflgYdYC9wgi4SP2Pg9InjvpGFA&s' alt="sdi" className="logo-image" />
            </div>
            <div className='login-container'>
                <button className="login-button" onClick={navigateToCreateUrl}>Login</button>
                <button className="login-button">Sign Up</button>
            </div>
        </div>
    );
};

export default Loginpage;