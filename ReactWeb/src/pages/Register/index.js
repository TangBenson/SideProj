import { useState } from 'react'
import './index.css';
import { Link } from 'react-router-dom';

const app = {
    color: 'red'
}

const Register = () => {

    return (
        <div>
            <h2>Register</h2>
            <button>
                <Link to="/">Go to Login</Link>
            </button>
        </div>
    )
}

export default Register