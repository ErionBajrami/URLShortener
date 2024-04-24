import React from 'react'
import UrlList from '../components/Url/UrlList'
import Navbar from '../components/Navbar/Navbar'
import Footer from '../components/Footer/Footer'

const UrlPage = () => {
  
  return (

    <div>
        <Navbar />
        <UrlList />
        <div style={{marginBottom: "200px"}}></div>
        <Footer />
    </div>
  )
}

export default UrlPage