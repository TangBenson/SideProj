import { useState } from 'react'
import './index.css';
// import { Link } from 'react-router-dom';

const app = {
    color: 'red'
}

const Login = () => {
    const handleLogin = () => {
        // 處理登入邏輯
    };

    return (
        <div className="login-page">
            <div className="background-image"></div>
            <div className="login-form">
                <h2>歡迎登入哀潤山</h2>
                <form>
                    <input type="text" placeholder="Username" />
                    <input type="password" placeholder="Password" />
                    <div className="button-container">
                        <button type="submit">登入</button>
                        <button>
                            註冊
                            {/* <Link to="/register">註冊</Link> */}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    )
}

export default Login