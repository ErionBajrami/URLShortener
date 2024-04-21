import React, { useState, useEffect} from 'react'
import axios from 'axios';
import "./Url.scss";

const UrlList = ({ userId }) => {
    const [urls, setUrls] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`http://localhost:7295/api/User/${userId}/urls`);
                setUrls(response.data.urls);
                console.log("response-data", response.data.urls);
            } catch (error) {
                console.log("Error fetching data: ", error);
            }
        };

        fetchData();
    }, [userId]);

    return (
        <div className="urlList-container">
            <h2>Previous Urls for User with id: {userId}</h2>
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
