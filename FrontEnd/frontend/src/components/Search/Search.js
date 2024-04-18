import React, {useState} from 'react'
import axios from 'axios'
import './Search.scss'

const Search = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [error, setError] = useState('');

  const handleSearch = async () => {
    try {
      const response = await axios.get(`https://localhost:7295/api/search?UrlName=${searchQuery}`);
      setSearchResults(response.data);
      setError('');
    }
    catch (error) {
      setSearchResults([]);
      setError("No matching URL's found");
    }
  }

  const handleChange = (event) => {
    const query = event.target.value;
    setSearchQuery(query);
    if (query === '') {
      setSearchResults([]);
    } else {
      handleSearch(query);
    }
  }
  return (
    <div className='searchContainer'>
       <h1>Search Page</h1>
      <div className='searchHeader'>
        <input type="text" value={searchQuery} onChange={handleChange} placeholder="Enter Url name to search"/>
        <button onClick={handleSearch}>Search</button>
      </div>
      {error && <p>{error}</p>}
      <div className='searchResults'>
        {searchResults.map((result) => (
          <div className='searchResult' key={result.shortUrl}>
            <p><strong>Original URL:</strong> {result.originalUrl}</p>
            <p><strong>Short URL:</strong> {result.shortUrl}</p>
            <p><strong>Number of Clicks:</strong> {result.nrOfClicks}</p>
          </div>
        ))}
      </div>
      <a href='\'>Go back to home</a>
    </div>
  )
}

export default Search;