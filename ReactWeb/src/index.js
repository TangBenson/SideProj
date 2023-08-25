import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import Login from './pages/Login';
import Register from './pages/Register';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" elememt={<Login/>}/>
        <Route path="/register" elememt={<Register/>}/>
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
);
