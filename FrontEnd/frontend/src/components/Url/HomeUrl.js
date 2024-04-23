import React, {useState, useEffect} from 'react'
import axios from 'axios';

const HomeUrl = () => {
    const [urls, setUrls] = useState([]);
    const [adminUrls, setAdminUrls] = useState([]);
    const [isAdmin, setIsAdmin] = useState(false);
    const token = localStorage.getItem('token');

    useEffect(() => {
        const fetchData = async () => {
          const isAdmin = await axios.get(`https://localhost:7295/api/User/isAdmin?token=${token}`);
          setIsAdmin((isAdmin.data));
          console.log('isAdmin', isAdmin);
            try {
              if(!(isAdmin.data)) {
                const response = await axios.get(`https://localhost:7295/Urls/${token}`);
                setUrls(response.data.urls);
              } else {
                const response = await axios.get('https://localhost:7295/api/User');
                console.log("response-data", response.data);
                setAdminUrls(response.data);
              }
            } catch (error) {
                console.log("Error fetching data: ", error);
            }
        };
        fetchData();
    }, [token]);
  return (
    <div className="urlList-container">
    {!isAdmin ? (<h2>Your Previous Urls</h2>) : (<h2>Users Previous Urls</h2>)}
    <div className="table-container">
      <table className="url-table">
        <thead>
          <tr>
            <th>Original URL</th>
            <th>Short Url</th>
            <th>Date Created</th>
          </tr>
        </thead>
        <tbody>
        {isAdmin ? (
            // Render admin table
            adminUrls.map((user) => {
                const rows = [];
                rows.push(
                    <tr key={`user-${user.id}`}>
                        <th colSpan="3">User ID: {user.id}</th>
                    </tr>
                );
                user.urls.forEach((url) => {
                    rows.push(
                        <tr key={`user-${user.id}-url-${url.id}`}>
                            <td>{url.originalUrl}</td>
                            <td>{"http://localhost:3000/" + url.shortUrl}</td>
                            <td>{url.dateCreated}</td>
                        </tr>
                    );
                });
                return rows;
            })
        ) : (
            // Render user table
            urls.map((url) => (
                <tr key={url.id}>
                    <td>{url.originalUrl}</td>
                    <td>{"http://localhost:3000/" + url.shortUrl}</td>
                    <td>{url.dateCreated}</td>
                </tr>
            ))
        )}
        </tbody>
      </table>
    </div>
  </div>
  )
}

export default HomeUrl