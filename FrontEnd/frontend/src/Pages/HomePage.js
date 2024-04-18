import React, { useState } from "react";
import Navbar from '../components/Navbar/Navbar'
import UrlShortener from "../components/Url/UrlShortener.js";

const HomePage = () => {
    const [url, setUrl] = useState("");

    const handleShorten = (shortenedUrl) => {
      setUrl(shortenedUrl);
    };
  return (
    <div>
        <Navbar/>
        <div style={{paddingTop: "100px"}}></div>
        <UrlShortener url={url} setUrl={setUrl} onShorten={handleShorten}/>
        <p style={{textAlign: "center"}}>Shortened URL: {url}</p>
    </div>
  )
}

export default HomePage