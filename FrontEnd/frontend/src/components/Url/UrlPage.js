import React from "react";
import axios from "axios";

const UrlShortener = ({ url, setUrl, onShorten }) => {
  const shortenUrl = async () => {
    try {
      const response = await axios.post(`http://localhost:5284/api/URL?url=${url}`);
      onShorten(response.data);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  const get = async () => {
    try {
      const response = await axios.get(`http://localhost:5284/${onShorten}`);
      window.location.href = response.data; // Redirect to the shortened URL
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div style={styles.container}>
      <input
        type="text"
        value={url}
        onChange={(e) => setUrl(e.target.value)}
        placeholder="Enter URL"
        style={styles.input}
      />
      <button style={styles.button} onClick={shortenUrl}>
        Shorten URL
      </button>
      <button style={styles.button} onClick={get}>
        Redirect
      </button>
    </div>
  );
};

const styles = {
  container: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    padding: "20px",
    backgroundColor: "#f9f9f9",
    borderRadius: "8px",
    boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
    maxWidth: "400px",
    margin: "0 auto",
  },
  input: {
    width: "100%",
    padding: "10px",
    marginBottom: "10px",
    fontSize: "16px",
    border: "1px solid #ccc",
    borderRadius: "4px",
    boxSizing: "border-box",
  },
  button: {
    padding: "10px 20px",
    fontSize: "16px",
    backgroundColor: "#007bff",
    color: "#fff",
    border: "none",
    borderRadius: "4px",
    cursor: "pointer",
    boxShadow: "0 2px 4px rgba(0, 123, 255, 0.2)",
    transition: "background-color 0.3s ease",
  },
};

export default UrlShortener;
