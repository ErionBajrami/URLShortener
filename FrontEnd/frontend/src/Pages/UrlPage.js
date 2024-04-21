import React from 'react'
import UrlList from '../Url/UrlList'
import Navbar from '../components/Navbar/Navbar'
const UrlPage = () => {
    
  return (
    <div>
        <Navbar />
        <UrlList userId={1} />
    </div>
  )
}

export default UrlPage