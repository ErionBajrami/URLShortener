import React from 'react'
import Register from '../components/Register/Register'
import Navbar from '../components/Navbar/Navbar'

const RegisterPage = () => {
  return (
    <div>
      <Navbar />
      <div style={{paddingTop: "120px"}}> 
        <Register/>
     </div>
    </div>
  )
}

export default RegisterPage