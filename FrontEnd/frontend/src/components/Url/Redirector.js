import React, { useEffect } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';

const Redirector = () => {
  const { shortUrl } = useParams(); // Extract shortUrl from route parameters

  useEffect(() => {
    const redirectToOriginalUrl = async () => {
      try {
        const response = await axios.get(`http://4.226.18.9:5001/${shortUrl}`);
        const originalUrl = response.data;

        // Redirect to the original URL
        window.location.replace(originalUrl);
      } catch (error) {
        console.error('Error redirecting:', error);
      }
    };

    redirectToOriginalUrl();
  }, [shortUrl]); // Run effect when shortUrl changes

  return <div>Redirecting...</div>; // Optional: Display a loading message
};

export default Redirector;
