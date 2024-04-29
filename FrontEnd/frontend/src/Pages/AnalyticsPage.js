import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Analytics from '../components/Analytics/Analytics';
import Navbar from '../components/Navbar/Navbar';
import Footer from '../components/Footer/Footer';

const AnalyticsPage = () => {
  const [isAdmin, setIsAdmin] = useState(false);
  const token = localStorage.getItem('token');

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Check if the user is an admin
        const isAdminResponse = await axios.get(`http://4.226.18.9:5001/api/User/isAdmin?token=${token}`);
        setIsAdmin(isAdminResponse.data);
      } catch (error) {
        console.log('Error fetching data: ', error);
      }
    };
    fetchData();
  }, [token]);

  return isAdmin ? (
    <div>
      <Navbar />
      <Analytics />
      <Footer />
    </div>
  ) : (
    <div style={{ fontWeight: 'bolder', fontSize: "23px", color: "red"}}>Unauthorized</div>
  );
};

export default AnalyticsPage;
