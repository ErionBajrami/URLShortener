import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Url.scss';

const UrlList = ({ userId }) => {
    const [urls, setUrls] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`http://localhost:5284/api/User/${userId}/urls`);
                setUrls(response.data.urls);
            } catch (error) {
                console.log('Error fetching data: ', error);
            }
        };

        fetchData();
    }, [userId]);

    const copyToClipboard = (text) => {
        navigator.clipboard.writeText(text)
            .then(() => alert('Copied to clipboard'))
            .catch((err) => console.error('Failed to copy:', err));
    };

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
                            <td style={{ width: '50%' }}>{url.originalUrl}</td>
                            <td>{"http://localhost:3000/" + url.shortUrl}</td>
                            <td>{url.nrOfClicks}</td>
                            <td><button onClick={() => copyToClipboard("http://localhost:3000/" + url.shortUrl)}>Copy</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UrlList;
