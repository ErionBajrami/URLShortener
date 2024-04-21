import React from 'react'
import UrlList from '../components/Url/UrlList'
import Navbar from '../components/Navbar/Navbar'

const UrlPage = () => {
  
  return (

    <div>
        <Navbar />
        <UrlList />
        <div style={{marginBottom: "200px"}}></div>
    </div>
  )
}

export default UrlPage