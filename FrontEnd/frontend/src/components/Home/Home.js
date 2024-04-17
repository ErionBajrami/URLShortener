import React from 'react';
import { useHistory } from 'react-router-dom';

const HomePage = () => {
  const history = useHistory();

  const navigateToCreateUrl = () => {
    history.push('/CreateUrlPage');
  };

  return (
    <div className="home-page">
      <header>
        <h1>Welcome to URL Shortener</h1>
      </header>
      <main>
        <button onClick={navigateToCreateUrl}>Create Short URL</button>
      </main>
      <footer>
        &copy; 2024 URL Shortener
      </footer>
    </div>
  );
};

export default HomePage;
