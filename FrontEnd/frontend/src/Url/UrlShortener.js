import React, {useState, useEffect} from "react";
import axios from "axios";
import "./Url.scss";

const UrlShortener = ({ url, setUrl, onShorten }) => {
  // const shortenUrl = async () => {
  //   try {
  //     const response = await axios.post(`http://localhost:5284/api/URL?url=${url}&userId=1`);
  //     onShorten(response.data);
  //   } catch (error) {
  //     console.error("Error fetching data:", error);
  //   }
  // };

  // const get = async () => {
  //   try {
  //     const response = await axios.get(`http://localhost:5284/${onShorten}`);
  //     // window.location.href = response.data; // Redirect to the shortened URL
  //     const longUrl = response.data;
  //     window.location.href = longUrl;
  //   } catch (error) {
  //     console.error("Error fetching data:", error);
  //   }
  // };
  const shortenUrl = async () => {
    try {
      const encodedUrl = encodeURIComponent(url);
      const response = await axios.post(`http://localhost:5284/api/URL?url=${encodedUrl}&userId=1`);
      onShorten(response.data); // Assuming response.data contains the shortened URL
    } catch (error) {
      console.error("Error shortening URL:", error);
    }
  };

  const get = async () => {
    try {
      const encodedUrl = encodeURIComponent(url);
      const response = await axios.get(`http://localhost:5284/${encodedUrl}`);
      const longUrl = response.data;

      // Check if the longUrl starts with a valid protocol (e.g., http:// or https://)
      if (isValidUrl(longUrl)) {
        window.location.replace(longUrl); // Redirect to the external URL
      } else {
        console.error("Invalid URL format:", longUrl);
      }
    } catch (error) {
      console.error("Error redirecting:", error);
    }
  };

  const isValidUrl = (url) => {
    return url.startsWith("http://") || url.startsWith("https://");
  };


  return (
    <div className="containerUrl">
      <input
        type="text"
        value={url}
        onChange={(e) => setUrl(e.target.value)}
        placeholder="Enter URL"
        className="inputUrl"
      />
      <button className="button" onClick={shortenUrl}>
        Shorten URL
      </button>
      <button className="button" onClick={get}>
        Redirect
      </button>
    </div>
  );
};


export default UrlShortener;
