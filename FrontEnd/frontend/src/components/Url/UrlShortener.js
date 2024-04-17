import React from "react";
import axios from "axios";
import "./Url.scss";

const UrlShortener = ({ url, setUrl, onShorten }) => {
  const shortenUrl = async () => {
    try {
      const response = await axios.post(`http://localhost:7295/api/URL?url=${url}`);
      onShorten(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const get = async () => {
    try {
      const response = await axios.get(`http://localhost:7295/${onShorten}`);
      window.location.href = response.data; // Redirect to the shortened URL
    } catch (error) {
      console.error("Error fetching data:", error);
    }
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
