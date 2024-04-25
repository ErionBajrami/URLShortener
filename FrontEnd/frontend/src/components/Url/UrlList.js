import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Url.scss';

const UrlList = ({ userId }) => {
    const [urls, setUrls] = useState([]);
    const [editingUrlId, setEditingUrlId] = useState(null);
    const [newDescription, setNewDescription] = useState('');
    const token = localStorage.getItem('token');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`http://localhost:5001/Urls/${token}`);
                setUrls(response.data.urls);
            } catch (error) {
                console.log('Error fetching data: ', error);
            }
        };

        fetchData();
    }, [token]);

    const handleEditDescription = async (id, newDescription) => {
      try {
        await axios.put(`http://localhost:5001/api/Url/${id}?description=${newDescription}`);
        //Update the description in the state
        const updatedUrls = urls.map(url => {
          if(url.id === id) {
            return {...url, description: newDescription};
          }
          return url;
        });
        setUrls(updatedUrls);
        setEditingUrlId(null);
      }catch (error) {
        console.log("Error updating description: ", error);
      }
    };


    return (
    <div className="urlList-container">
      <h2>Your Previous Urls</h2>
      <div className="table-container">
        <table className="url-table">
          <thead>
            <tr>
              <th>Original URL</th>
              <th>Short Url</th>
              <th>Date Created</th>
              <th>Description</th>
              <th>Edit</th>
            </tr>
          </thead>
          <tbody>
            {urls.map((url) => (
              <tr key={url.id}>
                <td>{url.originalUrl}</td>
                <td>{"http://localhost:3000/" + url.shortUrl}</td>
                <td>{url.dateCreated}</td>
                <td>
                  {editingUrlId === url.id ? (
                    <input type="text" value={newDescription} 
                    onChange={(e) => setNewDescription(e.target.value)}
                    className="inputUrl"
                    />
                  ) : (
                    url.description
                  )}
                </td>
                <td>
                  {editingUrlId === url.id ? (
                    <button onClick={() => handleEditDescription(url.id, newDescription)} id="doneBtn">Done</button>
                  ) : (
                    <button onClick={() => {
                      setEditingUrlId(url.id);
                      setNewDescription(url.description);
                    }} id="editBtn">Edit</button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
    );};

export default UrlList;
