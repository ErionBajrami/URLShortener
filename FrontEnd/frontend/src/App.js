import React, { useState } from "react";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Footer from "./components/Footer/Footer";
import HomePage from "./Pages/HomePage";
import UrlPage from "./Pages/UrlPage";
import SearchPage from "./Pages/SearchPage";
import LoginPage from "./Pages/LoginPage";
import Redirector from "./components/Url/Redirector";
import PrivateOutlet from "./PrivateOutlet";
import RegisterPage from "./Pages/RegisterPage";

function App() {
  const [url, setUrl] = useState("");
  console.log('Token stored:', localStorage.getItem('token'));

  const handleShorten = (shortenedUrl) => {
    setUrl(shortenedUrl);
  };

  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/login" element={<LoginPage />}/>
          <Route path="/register" element={<RegisterPage />}/>
          <Route path="/" element={<HomePage />}/>
          <Route path="/" element={<PrivateOutlet />}>
            <Route index element={<HomePage />} />
            <Route path="/:shortUrl" element={<Redirector />} />
            <Route path="/urls" element={<UrlPage />} />
            <Route path="/search" element={<SearchPage />} />
          </Route>
        </Routes>
        <Footer />
      </Router>
    </div>
  );
}

export default App;