import React from 'react';
import Search from '../components/Search/Search';
import Navbar from '../components/Navbar/Navbar';
import Footer from '../components/Footer/Footer';


const SearchPage = () => {
  return (
    <div>
      <Navbar />
      <Search/>
      <div style={{marginBottom: "200px"}}></div>
      <Footer />
    </div>
  )
}

export default SearchPage