import React, {useState} from 'react'
import axios from 'axios'
import './Search.scss'
import { Link } from 'react-router-dom';
const Search = () => {
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState([]);
  const [error, setError] = useState('');

  const handleSearch = async () => {
    try {
      const response = await axios.get(`http://localhost:5284/api/search?UrlName=${searchQuery}`);
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
            <p><strong>Short URL:</strong> {"http://localhost:3000/" + result.shortUrl}</p>
            <p><strong>Number of Clicks:</strong> {result.nrOfClicks}</p>
          </div>
        ))}
      </div>
      <Link to='\'>Go back to home</Link>
    </div>
  )
}

export default Search;