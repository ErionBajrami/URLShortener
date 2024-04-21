import React, { useState, useEffect} from 'react'
import axios from 'axios';
import "./Url.scss";

const UrlList = () => {
    const [urls, setUrls] = useState([]);
    const token = localStorage.getItem('token');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`https://localhost:7295/api/Urls/${token}`);
                setUrls(response.data.urls);
                console.log("response-data", response.data.urls);
            } catch (error) {
                console.log("Error fetching data: ", error);
            }
        };

        fetchData();
    }, [token]);

    return (
        <div className="urlList-container">
            <h2>Your Previous Urls</h2>
            <table className="url-table">
                <thead>
                    <tr>
                        <th>Original URL</th>
                        <th>Short Url</th>
                        <th>Number of Clicks</th>
                    </tr>
                </thead>
                <tbody>
                    {urls.map((url) => (
                        <tr key={url.id}>
                            <td>{url.originalUrl}</td>
                            <td>{"http://localhost:3000/" + url.shortUrl}</td>
                            <td>{url.nrOfClicks}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UrlList;
