import React from 'react'
import axios from 'axios';

const UrlDisplay = () => {
const get = async () => {
    try {
        const response = await axios.get('https://localhost:7295/api/URL');
        console.log(response.data);
    }
    catch(error) {
        console.log("Error fetching data", error); 
    }
    }
  return (
    <div>UrlDisplay</div>
  )
}

export default UrlDisplay