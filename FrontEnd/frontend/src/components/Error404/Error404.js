import React from 'react';
import { Link } from 'react-router-dom';
import './Error404.scss'

const ErrorComponent = () => {
  return (
    <div className="error-container">
      <img src= "/static/images/error404.png" alt="Error" className="error-image" loading='lazy'/>
      <h2>SOMETHING WENT WRONG, THE PAGE YOU ARE LOOKING FOR DOES NOT EXIST!</h2>
      <Link to="/" className='link'>Home Page</Link>
    </div>
  );
}

export default ErrorComponent;