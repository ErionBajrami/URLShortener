import React, { useState } from "react";
import Navbar from '../components/Navbar/Navbar'
import UrlShortener from "../components/Url/UrlShortener";
import UrlList from "../components/Url/UrlList";

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
        <UrlList/>
        <div style={{marginBottom: "200px"}}></div>
    </div>
  )
}

export default HomePage