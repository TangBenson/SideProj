import React, { useState } from 'react'
import './index.css';

const app = {
    color: 'red'
}

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');

    const handleRegister = () => {
        // Call API for registration
        // ...
    };

    return (
        <div>
            <h1>Register Page</h1>
                <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username" />
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} placeholder="Password" />
                <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} placeholder="Email" />
                <input type="tel" value={phone} onChange={(e) => setPhone(e.target.value)} placeholder="Phone" />
                <button onClick={handleRegister}>Register</button>
        </div>
    )
}

export default Register