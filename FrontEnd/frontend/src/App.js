import React, { useState } from 'react';
import Homepage from './components/HomePage/Homepage';

function App() {
  // return (
  //   <div className="App">
  //       <Homepage />
  //   </div>
  // );
  const [longUrl, setLongUrl] = useState('');
  const [shortUrl, setShortUrl] = useState('');
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    setShortUrl(longUrl)
  };
  
  return (
    <div className="container mx-auto mt-8">
      <h1 className="text-2xl font-bold mb-4">URL Shortener</h1>
      <form onSubmit={handleSubmit} className="flex">
        <input
          type="text"
          placeholder="Paste your long URL here"
          className="mr-2 p-2 border border-gray-400 rounded"
          value={longUrl}
          onChange={(e) => setLongUrl(e.target.value)}
        />
        <button type="submit" className="bg-blue-500 text-white px-4 py-2 rounded">
          Shorten
        </button>
      </form>
      {shortUrl && (
        <div className="mt-4">
          <p className="font-bold">Short URL:</p>
          <a href={shortUrl} target="_blank" rel="noopener noreferrer" className="text-blue-500">{shortUrl}</a>
        </div>
      )}
    </div>
  );
};

export default App;
