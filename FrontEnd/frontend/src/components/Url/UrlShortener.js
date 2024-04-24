import React, {useState} from "react";
import axios from "axios";
import "./Url.scss";

const UrlShortener = ({ url, setUrl, onShorten }) => {
  const [description, setDescription] = useState("");

  const shortenUrl = async () => {
    const token = localStorage.getItem('token');
    try {
      const encodedUrl = encodeURIComponent(url);
      const encodedDescription = encodeURIComponent(description); // Encode the description
      const response = await axios.post(
        `https://localhost:7295/api/URL?url=${encodedUrl}&token=${token}&description=${encodedDescription}`
      );
      onShorten(response.data);
    } catch (error) {
      console.error("Error shortening URL:", error);
    }
  };

  const redirectToOriginalUrl = async () => {
    try {
      const encodedUrl = encodeURIComponent(url);
      const response = await axios.get(`https://localhost:7295/${encodedUrl}`);
      console.log('encodedUrl', encodedUrl);
      const longUrl = response.data;

      // Check if the longUrl starts with a valid protocol (e.g., http:// or https://)
      if (isValidUrl(longUrl)) {
        window.open(longUrl, '_blank');// Redirect to the original URL
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
      <input
        type="text"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        placeholder="Enter Description"
        className="inputUrl" 
      />
      <button className="button" onClick={shortenUrl}>
        Shorten URL
      </button>
      <button className="button" onClick={redirectToOriginalUrl}>
        Redirect
      </button>
  
    </div>
  );
};


export default UrlShortener;
