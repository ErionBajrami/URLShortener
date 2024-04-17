import React, { useState } from "react";
import UrlShortener from "./components/CreateUrlPage/CreateUrlPage.js";

function App() {
  const [url, setUrl] = useState("");

  const handleShorten = (shortenedUrl) => {
    setUrl(shortenedUrl);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>My React App</h1>
      </header>
      <main className="App-body">
        <p>This is the body of the app.</p>
      </main>
      <footer className="App-footer">
        <UrlShortener url={url} setUrl={setUrl} onShorten={handleShorten} />
        <p>Shortened URL: {url}</p>
      </footer>
    </div>
  );
}

export default App;