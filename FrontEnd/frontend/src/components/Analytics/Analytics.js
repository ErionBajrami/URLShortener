import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Analytics.scss';

const UrlList = () => {
  const [urls, setUrls] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5001/api/URL');
        setUrls(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchData();
  }, []);

  const handleDeleteUrl = async (id) => {
    try {
      await axios.delete(`http://localhost:5001/api/URL/${id}`);
      setUrls(urls.filter(url => url.id !== id));
    } catch (error) {
      console.error('Error deleting URL:', error);
    }
  };

  const [users, setUsers] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5001/api/User');
        setUsers(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
    fetchData();
  }, []);

  const handleDeleteUser = async (id) => {
    try {
      await axios.delete(`http://localhost:5001/api/User?id=${id}`);
      setUsers(users.filter(user => user.id !== id));
    } catch (error) {
      console.error('Error deleting user:', error);
    }
  };

  return (
    <div>
        <div className="container">
        <h2>URL List</h2>
            <table className="table">
                <thead>
                    <tr>
                    <th>Original URL</th>
                    <th>Short URL</th>
                    <th>Description</th>
                    <th>User ID</th>
                    <th>Number of Clicks</th>
                    <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {urls.map(url => (
                    <tr key={url.id}>
                        <td>{url.originalUrl}</td>
                        <td>{url.shortUrl}</td>
                        <td>{url.description}</td>
                        <td>{url.userId}</td>
                        <td>{url.nrOfClicks}</td>
                        <td>
                        <button onClick={() => handleDeleteUrl(url.id)}>Delete</button>
                        </td>
                    </tr>
                    ))}
                </tbody>
            </table>
        </div>
        <div className="container" style={{marginBottom: "200px"}}>
        <h2>User List</h2>
        <table className="table">
          <thead>
            <tr>
              <th>User ID</th>
              <th>Email</th>
              <th>Full Name</th>
              <th>Created At</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {users.map(user => (
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.email}</td>
                <td>{user.fullName}</td>
                <td>{user.createdAt}</td>
                <td>
                  <button onClick={() => handleDeleteUser(user.id)}>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
);
};


export default UrlList;
