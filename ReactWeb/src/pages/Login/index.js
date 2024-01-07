import React, { useState, useEffect } from 'react'
import './index.css';
import { Link } from 'react-router-dom';
import { GoogleOAuthProvider, GoogleLogin } from '@react-oauth/google';


const app = {
    color: 'red'
}

const Login = () => {
    console.log("345");
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        console.log("-----");
    }, []);

    const handleGoogleLogin = (response) => {
        // 若是imloict模式則要在此处理登录逻辑，例如发送令牌到后端进行验证
        console.log(response);
    };

    return (
        <GoogleOAuthProvider clientId="862126110061-7fdcdk1at1q3l36gmee7rduq38q5n2qs.apps.googleusercontent.com">
            <div className="login-page">
                <div className="background-image"></div>
                <div className="login-form">
                    <h2>歡迎登入哀潤山</h2>
                    <form>
                        <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username" />
                        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
                        <div className="button-container">
                            <button>
                                <Link to="/map">登入</Link>
                            </button>
                            <button>
                                <Link to="/register">註冊</Link>
                            </button>
                            {/* Google OAuth 登录按钮 */}
                            <GoogleLogin
                                onSuccess={handleGoogleLogin}
                                onError={() => console.log('Login Failed')}
                                scope="profile email"
                                render={renderProps => (
                                    <button style={app} onClick={renderProps.onClick} disabled={renderProps.disabled}>OAuth</button>
                                )}
                            />
                        </div>
                    </form>
                </div>
            </div>
        </GoogleOAuthProvider>
    )
}

export default Login