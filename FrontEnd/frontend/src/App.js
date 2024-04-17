import React, { useState } from "react";
import UrlShortener from "./components/Url/UrlPage.js";
import Navbar from "./components/Navbar/Navbar.js";

function App() {
  const [url, setUrl] = useState("");

  const handleShorten = (shortenedUrl) => {
    setUrl(shortenedUrl);
  };

  return (
    <div className="App">
      <Navbar/>
      <div style={{paddingTop: "100px"}}></div>
      <UrlShortener url={url} setUrl={setUrl} onShorten={handleShorten}/>
      <p style={{textAlign: "center"}}>Shortened URL: {url}</p>
    </div>
  );
}

export default App;