import React from 'react';
import Search from '../components/Search/Search';
import Navbar from '../components/Navbar/Navbar';


const SearchPage = () => {
  return (
    <div>
      <Navbar />
      <Search/>
      <div style={{marginBottom: "200px"}}></div>
    </div>
  )
}

export default SearchPage