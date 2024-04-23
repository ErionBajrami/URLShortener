import React, {useState} from "react";
import axios from "axios";
import "./Url.scss";

const UrlShortener = ({ url, setUrl, onShorten }) => {
  const [description, setDescription] = useState("");
  const [urls, setUrls] = useState([]);
  const token  = localStorage.getItem('token');

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
    </div>
  );
};


export default UrlShortener;
