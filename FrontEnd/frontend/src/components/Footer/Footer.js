import React from 'react'
import "./Footer.scss";

const Footer = () => {
  return (
    <footer className="footer">
            <div>
      <p>&copy; 2024 URLShortener</p>
      </div>
      <div className='footer__links'>
        <ul>
          <li>
            <a href='https://google.com'> Terms & Conditions</a>
          </li>
          <li>
            <a href='https://google.com'>Privacy</a>
          </li>
        </ul>
      </div>
    </footer>
  )
}

export default Footer