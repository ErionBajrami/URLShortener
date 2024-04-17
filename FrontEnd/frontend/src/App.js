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
      <footer className="App-footer">
        <UrlShortener url={url} setUrl={setUrl} onShorten={handleShorten} />
        <p>Shortened URL: {url}</p>
      </footer>
    </div>
  );
}

export default App;