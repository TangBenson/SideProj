import React, { useState } from 'react'
import './index.css';
import { Link } from 'react-router-dom';
// import { Link } from 'react-router-dom';

const app = {
    color: 'red'
}

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = () => {
        // 處理登入邏輯，成功後導向地圖頁面，Call API for authentication
        console.log('handleLogin');
    };

    return (
        <div className="login-page">
            <div className="background-image"></div>
            <div className="login-form">
                <h2>歡迎登入哀潤山</h2>
                <form>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username" />
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
                    <div className="button-container">
                        {/* <button onClick={handleLogin}>登入</button> */}
                        <button>
                            <Link to="/map">登入</Link>
                        </button>
                        <button>
                            <Link to="/register">註冊</Link>
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default Login