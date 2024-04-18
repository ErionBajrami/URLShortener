import React, { useState } from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar/Navbar";
import Footer from "./components/Footer/Footer";
import HomePage from "./Pages/HomePage";
import UrlPage from "./Pages/UrlPage";
import SearchPage from "./Pages/SearchPage";
import LoginPage from "./Pages/LoginPage";

function App() {
  const [url, setUrl] = useState("");

  const handleShorten = (shortenedUrl) => {
    setUrl(shortenedUrl);
  };

  return (
    <div className="App">
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<HomePage />}/>
          <Route path="/urls" element={<UrlPage />}/>
          <Route path="/search" element={<SearchPage />}/>
          <Route path="/login" element={<LoginPage />}/>
        </Routes>
        <Footer />
      </Router>
    </div>
  );
}

export default App;